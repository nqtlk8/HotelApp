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
    public static class GetBookingIDbyNameDAL
    {
        public static async Task<string> GetBookingIDbyFullname(string fullname)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT BookingID FROM Booking WHERE Fullname = @Fullname";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Thêm tham số
                        command.Parameters.AddWithValue("@Fullname", fullname);

                        // Sử dụng ExecuteReaderAsync thay vì Task.Run
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Kiểm tra nếu có dữ liệu
                            if (reader.Read())
                            {
                                return reader["BookingID"].ToString();
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
