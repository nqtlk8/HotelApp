using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class InvoiceRoomDetailDAL
    {
        public static async Task<List<InvoiceRoomDetail>> GetRoomDetailsByInvoiceId(int invoiceID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT * FROM InvoiceRoomDetail WHERE InvoiceID = @InvoiceID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", invoiceID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                List<InvoiceRoomDetail> details = new List<InvoiceRoomDetail>();
                                do
                                {
                                    InvoiceRoomDetail detail = new InvoiceRoomDetail(
                                        Convert.ToInt32(reader["InvoiceRoomDetailID"]),
                                        Convert.ToInt32(reader["InvoiceID"]),
                                        Convert.ToInt32(reader["RoomID"]),
                                        Convert.ToDouble(reader["RoomPrice"]),
                                        Convert.ToInt32(reader["Nights"])
                                    );
                                    details.Add(detail);
                                } while (reader.Read());

                                return details;
                            }
                            else
                            {
                                
                                MessageBox.Show("❌ Lỗi null");
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy chi tiết hóa đơn: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    
    public static async Task InsertInvoiceRoomDetailAsync(InvoiceRoomDetail detail)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null)
                {
                    MessageBox.Show("❌ Không thể kết nối tới cơ sở dữ liệu.");
                    return;
                }

                try
                {
                    string query = @"
                        INSERT INTO InvoiceRoomDetail
                            (InvoiceID, RoomID, RoomPrice, Nights)
                        VALUES
                            (@InvoiceID, @RoomID, @RoomPrice, @Nights)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", detail.InvoiceID);
                        command.Parameters.AddWithValue("@RoomID", detail.RoomID);
                        command.Parameters.AddWithValue("@RoomPrice", detail.RoomPrice);
                        command.Parameters.AddWithValue("@Nights", detail.Nights);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi chèn chi tiết phòng: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }
    } 
}

