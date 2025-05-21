using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class ChartDAL
    {
        // Get occupancy rate by date range and grouping
        public static async Task<DataTable> GetOccupancyRateByDateRange(DateTime startDate, DateTime endDate, string timeGrouping)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Period", typeof(string));
            dataTable.Columns.Add("RoomType", typeof(string));
            dataTable.Columns.Add("OccupancyRate", typeof(double));

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return dataTable;

                try
                {
                    string dateFormat = timeGrouping == "month" ? "%Y-%m" : "%Y-%m-%d";

                    // Simpler query avoiding the complex join that might cause index errors
                    string query = $@"
                        WITH RoomCounts AS (
                            SELECT RoomType, COUNT(*) AS TotalRooms
                            FROM RoomInfo
                            GROUP BY RoomType
                        ),
                        BookedRooms AS (
                            SELECT 
                                strftime('{dateFormat}', b.Checkin) AS Period,
                                ri.RoomType,
                                COUNT(*) AS BookedCount
                            FROM Booking b
                            JOIN BookingRoom br ON b.BookingID = br.BookingID
                            JOIN RoomInfo ri ON br.RoomID = ri.RoomID
                            WHERE date(b.Checkin) >= date(?) AND date(b.Checkin) <= date(?)
                            GROUP BY Period, ri.RoomType
                        )
                        SELECT 
                            br.Period,
                            br.RoomType,
                            CAST(br.BookedCount AS REAL) / rc.TotalRooms * 100 AS OccupancyRate
                        FROM BookedRooms br
                        JOIN RoomCounts rc ON br.RoomType = rc.RoomType
                        ORDER BY br.Period, br.RoomType;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy tỷ lệ sử dụng phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return dataTable;
        }

        // Get top loyal customers
        public static async Task<DataTable> GetLoyalCustomers(DateTime startDate, DateTime endDate, int topCount)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("BookingCount", typeof(int));
            dataTable.Columns.Add("TotalSpent", typeof(decimal));

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return dataTable;

                try
                {
                    // First, let's check if the Guest table has a FullName column
                    DataTable schemaTable = connection.GetSchema("Columns", new[] { null, null, "Guest", null });
                    bool hasFullNameColumn = false;

                    foreach (DataRow row in schemaTable.Rows)
                    {
                        if (row["COLUMN_NAME"].ToString() == "FullName")
                        {
                            hasFullNameColumn = true;
                            break;
                        }
                    }

                    string nameColumn = hasFullNameColumn ? "FullName" : "FullName"; // Fallback to FullName or try another name if needed

                    string query = @"
                        SELECT 
                            g.FullName AS CustomerName,
                            COUNT(b.BookingID) AS BookingCount,
                            SUM(COALESCE(i.TotalPayment, b.TotalPrice)) AS TotalSpent
                        FROM Booking b
                        JOIN Guest g ON b.GuestID = g.GuestID
                        LEFT JOIN Invoice i ON b.BookingID = i.BookingID
                        WHERE date(b.Checkin) >= date(?) AND date(b.Checkin) <= date(?)
                        GROUP BY b.GuestID, g.FullName
                        ORDER BY BookingCount DESC, TotalSpent DESC
                        LIMIT ?;";


                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param3", topCount);

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy khách hàng thân thiết: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return dataTable;
        }

        // Get service performance
        public static async Task<DataTable> GetServicePerformance(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ServiceName", typeof(string));
            dataTable.Columns.Add("UsageCount", typeof(int));
            dataTable.Columns.Add("TotalRevenue", typeof(decimal));

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return dataTable;

                try
                {
                    // First check if the tables and columns exist
                    bool tablesExist = true;

                    try
                    {
                        using (var checkCommand = new SQLiteCommand(
                            "SELECT name FROM sqlite_master WHERE type='table' AND name IN ('Invoice', 'InvoiceServiceDetail', 'ServiceInfo')",
                            connection))
                        {
                            using (var reader = checkCommand.ExecuteReader())
                            {
                                HashSet<string> tables = new HashSet<string>();
                                while (reader.Read())
                                {
                                    tables.Add(reader.GetString(0));
                                }

                                tablesExist = tables.Count == 3;
                            }
                        }
                    }
                    catch
                    {
                        tablesExist = false;
                    }

                    if (tablesExist)
                    {
                        string query = @"
                            SELECT 
                                si.ServiceName,
                                SUM(isd.Quantity) AS UsageCount,
                                SUM(isd.ServicePrice * isd.Quantity) AS TotalRevenue
                            FROM Invoice i
                            JOIN InvoiceServiceDetail isd ON i.InvoiceID = isd.InvoiceID
                            JOIN ServiceInfo si ON isd.ServiceID = si.ServiceID
                            WHERE date(i.InvoiceDate) >= date(?) AND date(i.InvoiceDate) <= date(?)
                            GROUP BY si.ServiceID
                            ORDER BY UsageCount DESC;";

                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));

                            using (var adapter = new SQLiteDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                    }
                    else
                    {
                        // Fallback - add dummy data for testing
                        DataRow row = dataTable.NewRow();
                        row["ServiceName"] = "No service data available";
                        row["UsageCount"] = 0;
                        row["TotalRevenue"] = 0;
                        dataTable.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy hiệu suất dịch vụ: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return dataTable;
        }

        // Get revenue by date range with time grouping
        public static async Task<DataTable> GetRevenueByDateRange(DateTime startDate, DateTime endDate, string timeGrouping)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Period", typeof(string));
            dataTable.Columns.Add("RoomRevenue", typeof(decimal));
            dataTable.Columns.Add("ServiceRevenue", typeof(decimal));
            dataTable.Columns.Add("TotalRevenue", typeof(decimal));

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return dataTable;

                try
                {
                    string dateFormat = timeGrouping == "month" ? "%Y-%m" : "%Y-%m-%d";

                    // Simplified query to avoid index errors
                    string query = $@"
                        WITH DatePeriods AS (
                            WITH RECURSIVE 
                            dates(date) AS (
                                SELECT date(?)
                                UNION ALL
                                SELECT date(date, '+1 day')
                                FROM dates
                                WHERE date < date(?)
                            )
                            SELECT strftime('{dateFormat}', date) AS Period FROM dates
                        ),
                        InvoiceData AS (
                            SELECT 
                                strftime('{dateFormat}', i.InvoiceDate) AS Period,
                                SUM(i.RoomTotal) AS RoomRevenue,
                                SUM(i.ServiceTotal) AS ServiceRevenue,
                                SUM(i.TotalPayment) AS TotalRevenue
                            FROM Invoice i
                            WHERE date(i.InvoiceDate) >= date(?) AND date(i.InvoiceDate) <= date(?)
                            GROUP BY Period
                        )
                        SELECT
                            dp.Period,
                            COALESCE(id.RoomRevenue, 0) AS RoomRevenue,
                            COALESCE(id.ServiceRevenue, 0) AS ServiceRevenue,
                            COALESCE(id.TotalRevenue, 0) AS TotalRevenue
                        FROM DatePeriods dp
                        LEFT JOIN InvoiceData id ON dp.Period = id.Period
                        ORDER BY dp.Period;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param3", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param4", endDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy doanh thu theo thời gian: " + ex.Message);

                    // Fallback to a simpler query if the advanced one fails
                    try
                    {
                        string dateFormat = timeGrouping == "month" ? "%Y-%m" : "%Y-%m-%d";
                        string simpleQuery = $@"
                            SELECT 
                                strftime('{dateFormat}', i.InvoiceDate) AS Period,
                                SUM(i.RoomTotal) AS RoomRevenue,
                                SUM(i.ServiceTotal) AS ServiceRevenue,
                                SUM(i.TotalPayment) AS TotalRevenue
                            FROM Invoice i
                            WHERE date(i.InvoiceDate) >= date(?) AND date(i.InvoiceDate) <= date(?)
                            GROUP BY Period
                            ORDER BY Period;";

                        using (var command = new SQLiteCommand(simpleQuery, connection))
                        {
                            command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));

                            using (var adapter = new SQLiteDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                    }
                    catch (Exception fallbackEx)
                    {
                        MessageBox.Show("❌ Lỗi khi thực hiện truy vấn dự phòng: " + fallbackEx.Message);
                    }
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return dataTable;
        }

        // Get stay duration statistics
        public static async Task<DataTable> GetStayDurationStatistics(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("DurationRange", typeof(string));
            dataTable.Columns.Add("BookingCount", typeof(int));
            dataTable.Columns.Add("PercentageOfTotal", typeof(double));

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return dataTable;

                try
                {
                    string query = @"
                        WITH StayDurations AS (
                            SELECT 
                                b.BookingID,
                                julianday(b.Checkout) - julianday(b.Checkin) AS Duration
                            FROM Booking b
                            WHERE date(b.Checkin) >= date(?) AND date(b.Checkin) <= date(?)
                        ),
                        DurationRanges AS (
                            SELECT
                                BookingID,
                                CASE
                                    WHEN Duration < 2 THEN '1 ngày'
                                    WHEN Duration < 4 THEN '2-3 ngày'
                                    WHEN Duration < 8 THEN '4-7 ngày'
                                    ELSE '7+ ngày'
                                END AS DurationRange
                            FROM StayDurations
                        ),
                        TotalCount AS (
                            SELECT COUNT(*) AS Total FROM DurationRanges
                        )
                        SELECT 
                            dr.DurationRange,
                            COUNT(*) AS BookingCount,
                            COUNT(*) * 100.0 / tc.Total AS PercentageOfTotal
                        FROM DurationRanges dr, TotalCount tc
                        GROUP BY dr.DurationRange
                        ORDER BY 
                            CASE dr.DurationRange
                                WHEN '1 ngày' THEN 1
                                WHEN '2-3 ngày' THEN 2
                                WHEN '4-7 ngày' THEN 3
                                ELSE 4
                            END;";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@param1", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@param2", endDate.ToString("yyyy-MM-dd"));

                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy thống kê thời gian lưu trú: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return dataTable;
        }
    }
}