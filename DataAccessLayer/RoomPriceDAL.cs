using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class RoomPriceDAL
    {
        public static async Task<double?> GetPriceByRoomId(int roomId, DateTime checkinDate)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"SELECT Price FROM RoomPrice 
                                     WHERE RoomID = @RoomID AND 
                                           StartDate <= @Date AND EndDate >= @Date";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@Date", checkinDate.ToString("yyyy-MM-dd"));

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
                    MessageBox.Show("❌ Lỗi khi lấy giá phòng: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
        public static async Task<List<RoomPrice>> GetAllPricesByRoomId(int roomId)
        {
            var prices = new List<RoomPrice>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return prices;

                try
                {
                    string query = @"SELECT RoomID, Price, StartDate, EndDate 
                                     FROM RoomPrice 
                                     WHERE RoomID = @RoomID 
                                     ORDER BY StartDate ASC";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var info = new RoomPrice
                                {
                                    RoomID = Convert.ToInt32(reader["RoomID"]),
                                    Price = Convert.ToDouble(reader["Price"]),
                                    StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                                    EndDate = DateTime.Parse(reader["EndDate"].ToString())
                                };

                                prices.Add(info);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách giá phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return prices;
        }

        public static async Task<bool> AddRoomPrice(RoomPrice priceInfo)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"INSERT INTO RoomPrice (RoomID, Price, StartDate, EndDate) 
                             VALUES (@RoomID, @Price, @StartDate, @EndDate)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", priceInfo.RoomID);
                        command.Parameters.AddWithValue("@Price", priceInfo.Price);
                        command.Parameters.AddWithValue("@StartDate", priceInfo.StartDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@EndDate", priceInfo.EndDate.ToString("yyyy-MM-dd"));

                        int affectedRows = await command.ExecuteNonQueryAsync();
                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi thêm giá phòng: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
        public static async Task<bool> IsPricePeriodOverlapping(int roomId, DateTime newStart, DateTime newEnd)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"SELECT COUNT(1) FROM RoomPrice
                             WHERE RoomID = @RoomID
                               AND (@NewStart <= EndDate AND @NewEnd >= StartDate)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@NewStart", newStart.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@NewEnd", newEnd.ToString("yyyy-MM-dd"));

                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;  // Nếu có bản ghi trùng => true
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi kiểm tra trùng khoảng thời gian giá: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        // Cập nhật giá phòng
        public static async Task<List<RoomPrice>> GetOverlappingPrices(int roomId, DateTime newStart, DateTime newEnd, int? excludePriceId = null)
        {
            var list = new List<RoomPrice>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return list;

                string query = @"SELECT PriceID, RoomID, StartDate, EndDate, Price FROM RoomPrice
                         WHERE RoomID = @RoomID
                           AND (@NewStart <= EndDate AND @NewEnd >= StartDate)";

                if (excludePriceId.HasValue)
                    query += " AND PriceID != @PriceID";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoomID", roomId);
                    command.Parameters.AddWithValue("@NewStart", newStart.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@NewEnd", newEnd.ToString("yyyy-MM-dd"));
                    if (excludePriceId.HasValue)
                        command.Parameters.AddWithValue("@PriceID", excludePriceId.Value);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new RoomPrice
                            {
                                PriceID = reader.GetInt32(0),
                                RoomID = reader.GetInt32(1),
                                StartDate = reader.GetDateTime(2),
                                EndDate = reader.GetDateTime(3),
                                Price = reader.GetDouble(4)
                            });
                        }
                    }
                }

                DatabaseConnector.Close(connection);
            }

            return list;
        }
        public static async Task<bool> UpdateRoomPriceDetails(int priceId, int roomId, DateTime startDate, DateTime endDate, double price)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"UPDATE RoomPrice
                             SET RoomID = @RoomID,
                                 StartDate = @StartDate,
                                 EndDate = @EndDate,
                                 Price = @Price
                             WHERE PriceID = @PriceID";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@PriceID", priceId);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log thay vì hiển thị MessageBox trong DAL
                    Console.Error.WriteLine("Lỗi DAL: " + ex.Message);
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
