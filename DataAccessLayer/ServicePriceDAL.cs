using Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class ServicePriceDAL
    {
        public static async Task<double?> GetPriceByUsedDate(int serviceId, DateTime usedDate)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"SELECT ServicePrice FROM ServicePrice 
                                     WHERE ServiceID = @ServiceID AND 
                                           StartDate <= @Date AND EndDate >= @Date";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", serviceId);
                        command.Parameters.AddWithValue("@Date", usedDate.ToString("yyyy-MM-dd"));

                        object result = await command.ExecuteScalarAsync();
                        if (result != null && double.TryParse(result.ToString(), out double price))
                        {
                            return price;
                        }
                        else
                        {
                            return null; // Không tìm thấy giá hợp lệ
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy giá dịch vụ: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }


            }
        }
        public static async Task<List<ServicePrice>> GetPricesByServiceIDAsync(int serviceID)
        {
            var prices = new List<ServicePrice>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return prices;

                try
                {
                    string query = "SELECT ServicePriceID, ServiceID, ServicePrice AS ServicePriceValue, StartDate, EndDate FROM ServicePrice WHERE ServiceID = @serviceID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@serviceID", serviceID);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var price = new ServicePrice
                                {
                                    ServicePriceID = reader.GetInt32(0),
                                    ServiceID = reader.GetInt32(1),
                                    ServicePriceValue = reader.GetDouble(2),
                                    StartDate = reader.GetDateTime(3),
                                    EndDate = reader.GetDateTime(4)
                                };
                                prices.Add(price);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy giá dịch vụ: " + ex.Message);
                }
            }

            return prices;
        }

        public static async Task<bool> InsertServicePriceAsync(int serviceId, double price, DateTime startDate, DateTime endDate)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    // Kiểm tra trùng thời gian
                    string checkOverlap = @"
                    SELECT COUNT(*) FROM ServicePrice
                    WHERE ServiceID = @ServiceID AND (
                        (@StartDate BETWEEN StartDate AND EndDate) OR
                        (@EndDate BETWEEN StartDate AND EndDate) OR
                        (StartDate BETWEEN @StartDate AND @EndDate)
                    );";

                    using (var checkCmd = new SQLiteCommand(checkOverlap, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@ServiceID", serviceId);
                        checkCmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                        checkCmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                        long conflict = (long)(await checkCmd.ExecuteScalarAsync());
                        if (conflict > 0)
                        {
                            MessageBox.Show("❌ Khoảng thời gian bị trùng với giá đã tồn tại.");
                            return false;
                        }
                    }

                    // Nếu không bị trùng thì chèn vào
                    string insertSql = @"
                    INSERT INTO ServicePrice (ServiceID, ServicePrice, StartDate, EndDate)
                    VALUES (@ServiceID, @Price, @StartDate, @EndDate);";

                    using (var insertCmd = new SQLiteCommand(insertSql, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@ServiceID", serviceId);
                        insertCmd.Parameters.AddWithValue("@Price", price);
                        insertCmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                        int rowsAffected = await insertCmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi thêm giá dịch vụ: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
        
    public static async Task<bool> UpdateServicePriceAsync(ServicePrice price)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"UPDATE ServicePrice 
                             SET ServiceID = @ServiceID, 
                                 ServicePrice = @Price, 
                                 StartDate = @StartDate, 
                                 EndDate = @EndDate 
                             WHERE ServicePriceID = @ServicePriceID";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", price.ServiceID);
                        command.Parameters.AddWithValue("@Price", price.ServicePriceValue);
                        command.Parameters.AddWithValue("@StartDate", price.StartDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@EndDate", price.EndDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@ServicePriceID", price.ServicePriceID);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi cập nhật giá: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
    }




