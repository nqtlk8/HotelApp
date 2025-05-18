using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class RoomPriceDAL
    {
        public static async Task<double?> GetPriceByRoomId(int roomId, DateTime checkinDate)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"SELECT Price FROM RoomPrice 
                                     WHERE RoomID = @RoomID AND 
                                           StartDate <= @Date AND EndDate >= @Date";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomID", roomId);
                        command.Parameters.AddWithValue("@Date", checkinDate.ToString("yyyy-MM-dd"));

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && double.TryParse(result.ToString(), out double price))
                        {
                            return price;
                        }
                        else
                        {
                            return null; // Không tìm thấy giá hợp lệ
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy giá phòng: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
