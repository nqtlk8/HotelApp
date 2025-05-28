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
        public static async Task<Booking> GetBookingByIdAsync(int bookingID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT * FROM Booking WHERE BookingID = @BookingID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", bookingID);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int guestId = reader["GuestID"] != DBNull.Value ? Convert.ToInt32(reader["GuestID"]) : 0;
                                string fullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : string.Empty;
                                DateTime checkin = reader["Checkin"] != DBNull.Value ? Convert.ToDateTime(reader["Checkin"]) : DateTime.MinValue;
                                DateTime checkout = reader["Checkout"] != DBNull.Value ? Convert.ToDateTime(reader["Checkout"]) : DateTime.MinValue;
                                double totalPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDouble(reader["TotalPrice"]) : 0;

                                return new Booking(bookingID, guestId, fullName, checkin, checkout, totalPrice);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy booking theo ID: {ex.Message}");
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
        public static async Task<List<Booking>> GetBookingsWithStayPeriodAsync()
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return new List<Booking>();

                try
                {
                    string query = @"
                SELECT DISTINCT b.BookingID, b.GuestID, b.FullName, 
                b.Checkin, b.Checkout, b.TotalPrice
                FROM Booking b
                INNER JOIN StayPeriod sp ON b.BookingID = sp.BookingID
                LEFT JOIN Invoice i ON b.BookingID = i.BookingID
                WHERE i.InvoiceID IS NULL;"
;

                    using (var command = new SQLiteCommand(query, connection))
                    {
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
                                ;
                                bookings.Add(booking);
                            }
                            return bookings;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách Booking có StayPeriod: " + ex.Message);
                    return new List<Booking>();
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

    }
}
