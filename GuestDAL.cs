using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using Entities;

namespace DataAccessLayer
{
    public class GuestDAL : DatabaseAccess
    {
        public List<Guest> GetAllGuests()
        {
            List<Guest> guests = new List<Guest>();

            try
            {
                // Kiểm tra kết nối đã được khởi tạo chưa
                if (_connection == null)
                {
                    throw new InvalidOperationException("Chưa khởi tạo kết nối. Hãy gọi InitializeConnection() trước.");
                }

                OpenConnection();

                string query = "SELECT GuestID, FullName, PhoneNumber, Email, GuestPrivateInf FROM Guest";
                using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                {
                    try
                    {
                        // Kiểm tra trước khi thực hiện câu lệnh
                        if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                        {
                            MessageBox.Show("Kết nối cơ sở dữ liệu chưa được mở hoặc không hợp lệ.");
                            return guests;
                        }

                        // Thực thi truy vấn và đọc dữ liệu
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    // Kiểm tra sự tồn tại của các cột và đọc giá trị
                                    string fullName = reader["FullName"] is DBNull ? "" : reader["FullName"].ToString();
                                    string phoneNumber = reader["PhoneNumber"] is DBNull ? "" : reader["PhoneNumber"].ToString();
                                    string email = reader["Email"] is DBNull ? "" : reader["Email"].ToString();
                                    string guestPrivateInf = reader["GuestPrivateInf"] is DBNull ? "" : reader["GuestPrivateInf"].ToString();
                                    string guestID = reader["GuestID"] is DBNull ? "" : reader["GuestID"].ToString();

                                    // Tạo đối tượng Guest và thêm vào danh sách
                                    Guest guest = new Guest(fullName, phoneNumber, email, guestPrivateInf)
                                    {
                                        GuestID = guestID
                                    };

                                    guests.Add(guest);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi khi đọc dữ liệu khách: " + ex.Message + "\n" + ex.StackTrace);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi thực thi câu lệnh: " + ex.Message + "\n" + ex.StackTrace);
                    }
                }


            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Lỗi khi lấy danh sách khách: " + ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                CloseConnection();
            }

            return guests;
        }
    }
}
