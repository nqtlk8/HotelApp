using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DataAccessLayer
{
    public static class DatabaseConnector
    {
        private static readonly string _dbPath = @"C:\Users\Pc\source\repos\HotelApp\Database\database.db";

        public static async Task<SQLiteConnection> ConnectAsync()
        {
            if (!File.Exists(_dbPath))
            {
                MessageBox.Show("❌ Không tìm thấy file CSDL: " + _dbPath);
                return null;
            }

            try
            {
                string connectionString = $"Data Source={_dbPath};Version=3;";
                var connection = new SQLiteConnection(connectionString);

                await Task.Run(() => connection.Open());

                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối: " + ex.Message);
                return null;
            }
        }
        public static void Close(SQLiteConnection connection)
        {
            connection?.Close();
        }
    }
}
