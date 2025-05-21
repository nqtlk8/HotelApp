using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using Entities;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class GetGuestDAL
    {
        public static async Task<List<Guest>> GetGuests()
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT * FROM Guest";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            List<Guest> guests = new List<Guest>();
                            while (await reader.ReadAsync())
                            {
                                Guest guest = new Guest(
                                    Convert.ToInt32(reader["GuestID"]),
                                    reader["FullName"].ToString(),
                                    reader["PhoneNumber"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["GuestPrivateInf"].ToString()
                                );
                                guests.Add(guest);
                            }
                            return guests;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy danh sách khách: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        public static async Task<Guest> GetGuestById(int guestId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = "SELECT * FROM Guest WHERE GuestID = @GuestID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GuestID", guestId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Guest(
                                    Convert.ToInt32(reader["GuestID"]),
                                    reader["FullName"].ToString(),
                                    reader["PhoneNumber"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["GuestPrivateInf"].ToString()
                                );
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
                    MessageBox.Show("❌ Lỗi khi lấy thông tin khách: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        public static async Task<int> SaveGuest(Guest guest)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return -1;
                try
                {
                    string query = @"INSERT INTO Guest (FullName, PhoneNumber, Email, GuestPrivateInf)
                                     VALUES (@FullName, @PhoneNumber, @Email, @GuestPrivateInf);
                                     SELECT last_insert_rowid();";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", guest.FullName);
                        command.Parameters.AddWithValue("@PhoneNumber", guest.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", guest.Email);
                        command.Parameters.AddWithValue("@GuestPrivateInf", guest.GuestPrivateInfo);

                        object result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lưu khách: " + ex.Message);
                    return -1;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        public static async Task<bool> UpdateGuest(Guest guest)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;
                try
                {
                    string query = @"UPDATE Guest SET 
                                        FullName = @FullName,
                                        PhoneNumber = @PhoneNumber,
                                        Email = @Email,
                                        GuestPrivateInf = @GuestPrivateInf
                                     WHERE GuestID = @GuestID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", guest.FullName);
                        command.Parameters.AddWithValue("@PhoneNumber", guest.PhoneNumber);
                        command.Parameters.AddWithValue("@Email", guest.Email);
                        command.Parameters.AddWithValue("@GuestPrivateInf", guest.GuestPrivateInfo);
                        command.Parameters.AddWithValue("@GuestID", guest.GuestID);

                        int rows = await command.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi cập nhật khách: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

        public static async Task<bool> DeleteGuest(int guestId)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;
                try
                {
                    string query = "DELETE FROM Guest WHERE GuestID = @GuestID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GuestID", guestId);
                        int rows = await command.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi xóa khách: " + ex.Message);
                    return false;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    }
}
