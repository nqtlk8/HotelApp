using Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class BookingServiceDAL
    {
        public static async Task<List<(int ServiceID, int Quantity, DateTime UsedDate)>> GetServicesByBookingId(int bookingId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT ServiceID, Quantity, UsedDate FROM BookingService WHERE BookingID = @BookingID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", bookingId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var services = new List<(int, int, DateTime)>();
                            while (await reader.ReadAsync())
                            {
                                int serviceId = Convert.ToInt32(reader["ServiceID"]);
                                int quantity = Convert.ToInt32(reader["Quantity"]);
                                DateTime usedDate = DateTime.Parse(reader["UsedDate"].ToString());
                                services.Add((serviceId, quantity, usedDate));
                            }
                            return services.Count > 0 ? services : null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách dịch vụ theo BookingID: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
        public static async Task<bool> InsertBookingServiceAsync(BookingService service)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"
                INSERT INTO BookingService (BookingID, ServiceID, Quantity, UsedDate)
                VALUES (@BookingID, @ServiceID, @Quantity, @UsedDate);
            ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", service.BookingID);
                        command.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                        command.Parameters.AddWithValue("@Quantity", service.Quantity);
                        command.Parameters.AddWithValue("@UsedDate", service.UsedDate);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi thêm BookingService: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
