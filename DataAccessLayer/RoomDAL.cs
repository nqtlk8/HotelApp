using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public static async Task<List<RoomInfo>> GetAllRoomsAsync()
        {
            var rooms = new List<RoomInfo>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return rooms;

                try
                {
                    string query = "SELECT * FROM RoomInfo";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var room = new RoomInfo
                            {
                                RoomID = Convert.ToInt32(reader["RoomID"]),
                                RoomType = reader["RoomType"].ToString(),
                                Capacity = Convert.ToInt32(reader["Capacity"]),
                                Description = reader["Description"].ToString(),
                                Status = reader["Status"].ToString()
                            };

                            rooms.Add(room);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi nếu cần
                    Console.WriteLine("❌ Lỗi khi truy vấn danh sách phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return rooms;
        }

        public static async Task<bool> AddRoom(RoomInfo room)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    string query = @"INSERT INTO RoomInfo (RoomID, RoomType, Capacity, Description, Status) 
                 VALUES (@id, @type, @capacity, @description, @status)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", room.RoomID);
                        command.Parameters.AddWithValue("@type", room.RoomType);
                        command.Parameters.AddWithValue("@capacity", room.Capacity);
                        command.Parameters.AddWithValue("@description", room.Description);
                        command.Parameters.AddWithValue("@status", room.Status);

                        await command.ExecuteNonQueryAsync();
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi thêm phòng: " + ex.Message);
                    return false;
                }
            }
        }

        public static async Task<bool> IsRoomIDExists(int roomID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    const string query = "SELECT COUNT(1) FROM RoomInfo WHERE RoomID = @RoomID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomID);
                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi kiểm tra RoomID: " + ex.Message);
                    return false;
                }
                finally
                {
                    
                     connection.Close();
                    // connection.Dispose();
                }
            }
        }
        public static async Task<bool> UpdateRoomIncludingID(int oldRoomID, RoomInfo room)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    // Kiểm tra nếu ID mới đã tồn tại
                    if (oldRoomID != room.RoomID)
                    {
                        string checkQuery = "SELECT COUNT(1) FROM RoomInfo WHERE RoomID = @newID";
                        using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@newID", room.RoomID);
                            var result = await checkCommand.ExecuteScalarAsync();
                            if (result != null && int.TryParse(result.ToString(), out int count) && count > 0)
                            {
                                MessageBox.Show("⚠️ RoomID mới đã tồn tại.");
                                return false;
                            }
                        }
                    }

                    string query = @"UPDATE RoomInfo 
                             SET RoomID = @newID,
                                 RoomType = @type, 
                                 Capacity = @capacity, 
                                 Description = @description, 
                                 Status = @status 
                             WHERE RoomID = @oldID";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newID", room.RoomID);
                        command.Parameters.AddWithValue("@type", room.RoomType);
                        command.Parameters.AddWithValue("@capacity", room.Capacity);
                        command.Parameters.AddWithValue("@description", room.Description);
                        command.Parameters.AddWithValue("@status", room.Status);
                        command.Parameters.AddWithValue("@oldID", oldRoomID);

                        int rows = await command.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi cập nhật RoomID: " + ex.Message);
                    return false;
                }
            }
        }

       
    }
}
