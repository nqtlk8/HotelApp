using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class DatabaseAccess
    {
        protected SQLiteConnection _connection;
        protected string _connectionString;

        // Constructor nhận đường dẫn tới cơ sở dữ liệu SQLite
        public void InitializeConnection()
        {
            _connectionString = @"Data Source=C:\Users\PC-ACER\demo3.db;Version=3;";
            _connection = new SQLiteConnection(_connectionString);
        }

        // Mở kết nối tới cơ sở dữ liệu
        public void OpenConnection()
        {
            try
            {
                if (_connection == null)
                {
                    MessageBox.Show("Chưa khởi tạo kết nối. Gọi InitializeConnection() trước.");
                    return;
                }

                // Kiểm tra trạng thái kết nối trước khi mở
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                    // Có thể bỏ qua thông báo này trong quá trình thực thi nhiều lần, chỉ thông báo lần đầu tiên
                    if (_connection.State == System.Data.ConnectionState.Open)
                    {
                        // Để kiểm tra chỉ lần đầu tiên hoặc khi cần thiết
                        MessageBox.Show("Kết nối tới cơ sở dữ liệu thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở kết nối: " + ex.Message);
            }
        }

        // Đóng kết nối tới cơ sở dữ liệu
        public void CloseConnection()
        {
            try
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    MessageBox.Show("Kết nối đã được đóng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đóng kết nối: " + ex.Message);
            }
        }
    }
}
