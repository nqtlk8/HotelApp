using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using Entities;
using System.Collections.Generic;

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
                                return new ServiceInfo(
                                    Convert.ToInt32(reader["ServiceID"]),
                                    reader["ServiceName"].ToString(),
                                    reader["Descrip"].ToString(),
                                    Convert.ToInt32(reader["IsActive"])
                                );
                            }
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi khi lấy dịch vụ:\n{ex.Message}\n\nChi tiết:\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
     
            public static async Task<List<ServiceInfo>> GetAllServices()
            {
                using (var connection = await DatabaseConnector.ConnectAsync())
                {
                    if (connection == null) return null;

                    try
                    {
                        string query = "SELECT ServiceID, ServiceName, Descrip, IsActive FROM ServiceInfo";
                        using (var command = new SQLiteCommand(query, connection))
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var services = new List<ServiceInfo>();

                            while (await reader.ReadAsync())
                            {
                                services.Add(new ServiceInfo(
                                    Convert.ToInt32(reader["ServiceID"]),
                                    reader["ServiceName"].ToString(),
                                    reader["Descrip"].ToString(),
                                    Convert.ToInt32(reader["IsActive"])
                                ));
                            }

                            return services;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi khi lấy danh sách dịch vụ: " + ex.Message);
                        return null;
                    }
                    finally
                    {
                        DatabaseConnector.Close(connection);
                    }
                }
            }

            public static async Task<bool> AddService(ServiceInfo service)
            {
                using (var connection = await DatabaseConnector.ConnectAsync())
                {
                    if (connection == null) return false;

                    try
                    {
                        string query = "INSERT INTO ServiceInfo (ServiceName, Descrip, IsActive) VALUES (@name, @descrip, @isActive)";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@name", service.ServiceName);
                            command.Parameters.AddWithValue("@descrip", service.Descrip);
                            command.Parameters.AddWithValue("@isActive", service.IsActive);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi khi thêm dịch vụ: " + ex.Message);
                        return false;
                    }
                }
            }

            public static async Task<bool> UpdateService(ServiceInfo service)
            {
                using (var connection = await DatabaseConnector.ConnectAsync())
                {
                    if (connection == null) return false;

                    try
                    {
                        string query = "UPDATE ServiceInfo SET ServiceName = @name, Descrip = @descrip, IsActive = @isActive WHERE ServiceID = @id";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@name", service.ServiceName);
                            command.Parameters.AddWithValue("@descrip", service.Descrip);
                            command.Parameters.AddWithValue("@isActive", service.IsActive);
                            command.Parameters.AddWithValue("@id", service.ServiceID);

                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi khi cập nhật dịch vụ: " + ex.Message);
                        return false;
                    }
                }
            }

            public static async Task<bool> DisableService(int serviceID)
            {
                using (var connection = await DatabaseConnector.ConnectAsync())
                {
                    if (connection == null) return false;

                    try
                    {
                        string query = "UPDATE ServiceInfo SET IsActive = 0 WHERE ServiceID = @id";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", serviceID);
                            await command.ExecuteNonQueryAsync();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi khi vô hiệu hóa dịch vụ: " + ex.Message);
                        return false;
                    }
                }
            }
        }
    }


