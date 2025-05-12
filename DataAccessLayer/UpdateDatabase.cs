using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class UpdateDatabase
    {
        public static async Task<int> Query(string query, SQLiteParameter[] parameters = null)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return -1;

                try
                {
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        return await Task.Run(() => command.ExecuteNonQuery());
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("❌ Lỗi khi thực thi câu lệnh: " + ex.Message);
                    return -1;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
