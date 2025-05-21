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
    public class InvoiceServiceDetailDAL
    {


        public static async Task<List<InvoiceServiceDetail>> GetServiceDetailsByInvoiceId(int invoiceID)
        {
            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = "SELECT * FROM InvoiceServiceDetail WHERE InvoiceID = @InvoiceID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", invoiceID);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                List<InvoiceServiceDetail> details = new List<InvoiceServiceDetail>();
                                do
                                {
                                    InvoiceServiceDetail detail = new InvoiceServiceDetail(
                                        Convert.ToInt32(reader["InvoiceServiceDetailID"]),
                                        Convert.ToInt32(reader["InvoiceID"]),
                                        Convert.ToInt32(reader["ServiceID"]),
                                        Convert.ToDouble(reader["ServicePrice"]),
                                        Convert.ToInt32(reader["Quantity"])
                                    );
                                    details.Add(detail);
                                } while (reader.Read());

                                return details;
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
                    MessageBox.Show("❌ Lỗi khi lấy chi tiết dịch vụ hóa đơn: " + ex.Message);
                    return null;
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }



        public static async Task InsertInvoiceServiceDetailAsync(InvoiceServiceDetail detail)
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
                        INSERT INTO InvoiceServiceDetail
                            (InvoiceID, ServiceID, ServicePrice, Quantity)
                        VALUES
                            (@InvoiceID, @ServiceID, @ServicePrice, @Quantity)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceID", detail.InvoiceID);
                        command.Parameters.AddWithValue("@ServiceID", detail.ServiceID);
                        command.Parameters.AddWithValue("@ServicePrice", detail.ServicePrice);
                        command.Parameters.AddWithValue("@Quantity", detail.Quantity);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi chèn chi tiết dịch vụ: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }
        }

    }
}

