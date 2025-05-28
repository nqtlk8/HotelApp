using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class GetRoomInfoDAL
    {
        public static async Task<List<RoomInfo>> GetRoomInfo()
        {
            var roomList = new List<RoomInfo>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    // Chỉ lấy những phòng có Status = 'Sẵn sàng'
                    string query = "SELECT * FROM RoomInfo WHERE Status = 'Sẵn sàng'";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var roomInfo = new RoomInfo(
                                Convert.ToInt32(reader["RoomID"]),
                                reader["RoomType"].ToString(),
                                Convert.ToInt32(reader["Capacity"]),
                                reader["Description"].ToString()
                            );
                                roomList.Add(roomInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy thông tin phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
                return roomList;
            }
        }
    }
}
