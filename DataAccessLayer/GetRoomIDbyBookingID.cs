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
    public static class GetRoomIDbyBookingID
    {
        public static async Task<List<string>> GetRoomIDbyBooking(string bookingid)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT RoomID FROM BookingRoom WHERE BookingID = @BookingID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Thêm tham số
                        command.Parameters.AddWithValue("@BookingID", bookingid);

                        // Sử dụng ExecuteReaderAsync thay vì Task.Run
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Kiểm tra nếu có dữ liệu
                            if (reader.Read())
                            {
                                List<string> roomIDs = new List<string>();
                                do
                                {
                                    roomIDs.Add(reader["RoomID"].ToString());
                                } while (reader.Read());
                                return roomIDs;
                            }
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
