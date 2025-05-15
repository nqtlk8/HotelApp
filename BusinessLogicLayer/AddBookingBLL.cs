using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer
{
    public static class AddBookingBLL
    {
        public async static Task<int> AddBooking(Booking booking, List<int> roomIds)
        {
            string bookingQuery = @"
            INSERT INTO Booking (GuestID, FullName, TotalPrice, Checkin, Checkout)
            VALUES (@GuestID, @FullName, @TotalPrice, @Checkin, @Checkout);
            SELECT last_insert_rowid();";

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return -1;

                try
                {
                    using (var command = new SQLiteCommand(bookingQuery, connection))
                    {
                        command.Parameters.AddWithValue("@GuestID", booking.GuestID);
                        command.Parameters.AddWithValue("@FullName", booking.FullName);
                        command.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                        command.Parameters.AddWithValue("@Checkin", booking.CheckInDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Checkout", booking.CheckOutDate.ToString("yyyy-MM-dd"));

                        long bookingId = (long)await command.ExecuteScalarAsync();
                        if (bookingId <= 0) return -1;

                        // Gán BookingID vào object đã truyền (nếu cần dùng tiếp)
                        booking.BookingID = (int)bookingId;

                        // Thêm dữ liệu vào bảng BookingRoom
                        foreach (int roomId in roomIds)
                        {
                            string bookingRoomQuery = "INSERT INTO BookingRoom (BookingID, RoomID) VALUES (@BookingID, @RoomID)";
                            using (var roomCmd = new SQLiteCommand(bookingRoomQuery, connection))
                            {
                                roomCmd.Parameters.AddWithValue("@BookingID", bookingId);
                                roomCmd.Parameters.AddWithValue("@RoomID", roomId);
                                await roomCmd.ExecuteNonQueryAsync();
                            }
                        }

                        return (int)bookingId;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("❌ Lỗi khi thêm booking: " + ex.Message);
                    return -1;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
