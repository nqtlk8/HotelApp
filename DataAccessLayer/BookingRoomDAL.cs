using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class BookingRoomDAL
    {
        public static async Task<List<int>> GetRoomIdsByBookingId(int bookingId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return new List<int>(); // Trả về danh sách rỗng thay vì null

                try
                {
                    string query = "SELECT RoomID FROM BookingRoom WHERE BookingID = @BookingID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", bookingId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<int> roomIds = new List<int>();
                            while (await reader.ReadAsync())
                            {
                                roomIds.Add(Convert.ToInt32(reader["RoomID"]));
                            }
                            return roomIds; // Trả về danh sách (có thể rỗng)
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách phòng theo BookingID: " + ex.Message);
                    return new List<int>(); // Trả về danh sách rỗng thay vì null
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
