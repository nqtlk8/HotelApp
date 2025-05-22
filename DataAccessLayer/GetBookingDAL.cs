using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class GetBookingDAL
    {
        public static async Task<List<Booking>> GetBooking()
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT BookingID, GuestID, FullName, Checkin, Checkout, TotalPrice FROM Booking";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var bookings = new List<Booking>();

                            while (await reader.ReadAsync())
                            {
                                bookings.Add(new Booking(
                                    Convert.ToInt32(reader["BookingID"]),
                                    Convert.ToInt32(reader["GuestID"]),
                                    reader["FullName"].ToString(),
                                    Convert.ToDateTime(reader["Checkin"]),
                                    Convert.ToDateTime(reader["Checkout"]),
                                    Convert.ToInt32(reader["TotalPrice"])
                                ));
                            }

                            if (bookings.Count == 0)
                            {
                                MessageBox.Show("❌ Không tìm thấy người dùng nào.");
                                return null;
                            }

                            return bookings;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy người dùng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }

                return null;
            }
        }
    }
}
