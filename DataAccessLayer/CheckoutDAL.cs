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
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
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
                            logger.Info($"Checkout thành công cho BookingID: {bookingID}");
                            MessageBox.Show("✅ Checkout thành công!");
                        }
                        else
                        {
                            logger.Warn($"Không tìm thấy dòng phù hợp để checkout cho BookingID: {bookingID}");
                            MessageBox.Show("⚠️ Không tìm thấy dòng phù hợp để checkout.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Lỗi khi thực hiện checkout");
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
