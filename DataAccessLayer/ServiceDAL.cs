using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class ServiceDAL
    {
        public static async Task<ServiceInfo> GetServiceByID(int serviceId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT ServiceID, ServiceName, Descrip FROM ServiceInfo WHERE ServiceID = @ServiceID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ServiceID", serviceId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                ServiceInfo service = new ServiceInfo(
                                    Convert.ToInt32(reader["ServiceID"]),
                                    reader["ServiceName"].ToString(),
                                    reader["Descrip"].ToString()
                                );
                                return service;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy thông tin dịch vụ: " + ex.Message);
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
