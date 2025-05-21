using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class ListNameSearch
    {
        public static async Task<List<Guest>> GetListName(string table)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = $"SELECT GuestID, Fullname FROM {table}"; 
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        // Sử dụng ExecuteReaderAsync thay vì Task.Run
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<Guest> names = new List<Guest>();
                            while (reader.Read())
                            {
                                names.Add(new Guest(Convert.ToInt32(reader["GuestID"]), reader["Fullname"].ToString()));
                            }
                            return names;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy tên: " + ex.Message);
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
