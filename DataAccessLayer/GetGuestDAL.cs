using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
                            if (reader.Read())
                            {
                                List<Guest> guests = new List<Guest>();
                                do
                                {
                                    Guest guest = new Guest(
                                        Convert.ToInt32(reader["GuestID"]),
                                        reader["FullName"].ToString(),
                                        reader["PhoneNumber"].ToString(),
                                        reader["Email"].ToString(),
                                        reader["GuestPrivateInf"].ToString()
                                    );
                                    guests.Add(guest);
                                } while (reader.Read());
                                return guests;
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
                    MessageBox.Show("❌ Lỗi khi lấy danh sách khách: " + ex.Message);
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
