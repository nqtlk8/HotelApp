using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class CheckoutDAL
    {
        public static async Task CheckoutAsync(int bookingID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null)
                {
                    MessageBox.Show("❌ Không kết nối được cơ sở dữ liệu.");
                    return;
                }

                try
                {
                    string query = @"
                    UPDATE StayPeriod 
                    SET CheckoutActual = @now 
                    WHERE BookingID = @bookingID AND CheckoutActual IS NULL
                ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@now", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@bookingID", bookingID);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("✅ Checkout thành công!");
                        }
                        else
                        {
                            MessageBox.Show("⚠️ Không tìm thấy dòng phù hợp để checkout.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi thực hiện checkout: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
