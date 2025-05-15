using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SQLite;

namespace DataAccessLayer
{
    public static class GetRoomTypeDAL
    {
        public static async Task<List<string>> GetRoomType()
        {
            using (var Connection = await DatabaseConnector.ConnectAsync() )
            {
                if (Connection == null) return null;
                try
                {
                    string query = "SELECT distinct RoomType FROM RoomInfo";
                    using (var command = new SQLiteCommand(query, Connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<string> roomTypes = new List<string>();
                            while (await reader.ReadAsync())
                            {
                                roomTypes.Add(reader["RoomType"].ToString());
                            }
                            return roomTypes;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Lỗi khi lấy danh sách loại phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(Connection);
                }
                return null;
            }    
        }
    }
}
