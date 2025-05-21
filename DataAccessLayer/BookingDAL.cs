using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Entities;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class BookingDAL
    {
        public static async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT * FROM Booking WHERE BookingID = @BookingID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", bookingId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int guestId = Convert.ToInt32(reader["GuestID"]);
                                string fullName = reader["FullName"].ToString();
                                DateTime checkin = Convert.ToDateTime(reader["CheckInDate"]);
                                DateTime checkout = Convert.ToDateTime(reader["CheckOutDate"]);
                                double totalPrice = Convert.ToDouble(reader["TotalPrice"]);

                                return new Booking(bookingId, guestId, fullName, checkin, checkout, totalPrice);
                            }
                            else return null;
                        }
                    }
                }
                catch
                {
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        public static async Task<List<Booking>> GetBookings()
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT * FROM Booking";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<Booking> bookings = new List<Booking>();
                        while (await reader.ReadAsync())
                        {
                            Booking booking = new Booking(
                                Convert.ToInt32(reader["BookingID"]),
                                Convert.ToInt32(reader["GuestID"]),
                                reader["FullName"].ToString(),
                                Convert.ToDateTime(reader["Checkin"]),
                                Convert.ToDateTime(reader["Checkout"]),
                                Convert.ToDouble(reader["TotalPrice"])
                            );
                            bookings.Add(booking);
                        }
                        return bookings;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách đặt phòng: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
        public static async Task<Booking> GetBookingByInvoiceIDAsync(int invoiceId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"
                SELECT b.*
                FROM Booking b
                INNER JOIN Invoice i ON b.BookingID = i.BookingID
                WHERE i.InvoiceID = @InvoiceID";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", invoiceId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Booking(
                                    Convert.ToInt32(reader["BookingID"]),
                                    Convert.ToInt32(reader["GuestID"]),
                                    reader["FullName"].ToString(),
                                    DateTime.Parse(reader["Checkin"].ToString()),
                                    DateTime.Parse(reader["Checkout"].ToString()),
                                    Convert.ToDouble(reader["TotalPrice"])
                                    );

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy thông tin booking từ InvoiceID: " + ex.Message);
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
