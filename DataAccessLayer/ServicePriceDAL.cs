using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class ServicePriceDAL
    {
        public static async Task<double?> GetPriceByServiceId(int serviceId, DateTime usedDate)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"SELECT ServicePrice FROM ServicePrice 
                                     WHERE ServiceID = @ServiceID AND 
                                           StartDate <= @Date AND EndDate >= @Date";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", serviceId);
                        command.Parameters.AddWithValue("@Date", usedDate.ToString("yyyy-MM-dd"));

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
                    MessageBox.Show("❌ Lỗi khi lấy giá dịch vụ: " + ex.Message);
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
