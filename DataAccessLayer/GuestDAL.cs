using Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public class GuestDAL
    {
        public static async Task<Guest> GetGuestByInvoiceIDAsync(int invoiceID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;
                try
                {
                    string query = @"
                SELECT g.GuestID, g.FullName, g.PhoneNumber, g.Email, g.GuestPrivateInf
                FROM Guest g
                INNER JOIN Booking b ON g.GuestID = b.GuestID
                INNER JOIN Invoice i ON b.BookingID = i.BookingID
                WHERE i.InvoiceID = @InvoiceID
            ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", invoiceID);

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
                    MessageBox.Show("❌ Lỗi khi lấy thông tin khách từ InvoiceID: " + ex.Message);
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
