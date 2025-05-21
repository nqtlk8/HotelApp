using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public static class RoomDAL
    {
        public static async Task<RoomInfo> GetRoomByID(int roomID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT RoomID, RoomType, Capacity, Description FROM Room WHERE RoomID = @RoomID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                RoomInfo room = new RoomInfo(
                                    Convert.ToInt32(reader["RoomID"]),
                                    reader["RoomType"].ToString(),
                                    Convert.ToInt32(reader["Capacity"]),
                                    reader["Description"].ToString()
                                );
                                return room;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("❌ Lỗi lấy thông tin phòng: " + ex.Message);
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
