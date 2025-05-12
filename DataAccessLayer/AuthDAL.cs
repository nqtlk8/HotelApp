using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class AuthDAL
    {
        public static async Task<User> GetUser(string name)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT Name, Pass, Role FROM User WHERE Name = @Name";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Thêm tham số
                        command.Parameters.AddWithValue("@Name", name);

                        // Sử dụng ExecuteReaderAsync thay vì Task.Run
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Kiểm tra nếu có dữ liệu
                            if (reader.Read())
                            {
                                return new User(
                                    reader["Name"].ToString(),
                                    reader["Pass"].ToString(),
                                    reader["Role"].ToString()
                                );
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
