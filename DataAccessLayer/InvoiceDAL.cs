using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public static class InvoiceDAL
    {
        public static async Task<int> InsertInvoiceAsync(Invoice invoice)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null)
                {
                    MessageBox.Show("❌ Không thể kết nối tới cơ sở dữ liệu.");
                    return 0;
                }

                try
                {
                    string query = @"
                INSERT INTO Invoice 
                    (BookingID, InvoiceDate, RoomTotal, ServiceTotal, VAT, Surcharge, TotalPayment) 
                VALUES 
                    (@BookingID, @InvoiceDate, @RoomTotal, @ServiceTotal, @VAT, @Surcharge, @TotalPayment);
                SELECT last_insert_rowid();";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", invoice.BookingID);
                        command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@RoomTotal", invoice.RoomTotal);
                        command.Parameters.AddWithValue("@ServiceTotal", invoice.ServiceTotal);
                        command.Parameters.AddWithValue("@VAT", invoice.VAT);
                        command.Parameters.AddWithValue("@Surcharge", invoice.Surcharge);
                        command.Parameters.AddWithValue("@TotalPayment", invoice.TotalPayment);

                        var result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int newId))
                        {
                            return newId;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi chèn hóa đơn: " + ex.Message);
                    return 0;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

       
            public static async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
            {
                using (var connection = await DatabaseConnector.ConnectAsync())
                {
                    if (connection == null) return null;

                    try
                    {
                        string query = "SELECT * FROM Invoice WHERE InvoiceID = @invoiceId";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@invoiceId", invoiceId);

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    Invoice invoice = new Invoice(
                                        Convert.ToInt32(reader["InvoiceID"]),
                                        Convert.ToInt32(reader["BookingID"]),
                                        Convert.ToDateTime(reader["InvoiceDate"]),
                                        Convert.ToDouble(reader["RoomTotal"]),
                                        Convert.ToDouble(reader["ServiceTotal"]),
                                        Convert.ToDouble(reader["VAT"]),
                                        Convert.ToDouble(reader["Surcharge"]),
                                        Convert.ToDouble(reader["TotalPayment"])
                                    );
                                    return invoice;
                                }
                                else
                                {
                                    return null; // Không tìm thấy hóa đơn
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi khi lấy hóa đơn: " + ex.Message);
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

