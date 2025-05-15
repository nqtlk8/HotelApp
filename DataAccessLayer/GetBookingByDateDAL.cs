using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer
{
    public static class GetBookingByDateDAL
    {
        public static async Task<List<int>> GetBookedRoom(DateTime SelectedStartDate, DateTime SelectedEndDate)
        {
            var roomIds = new List<int>();

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return null;

                try
                {
                    string query = @"
                    SELECT br.RoomID
                    FROM BookingRoom br
                    JOIN Booking b ON br.BookingID = b.BookingID
                    WHERE b.CheckIn < @SelectedEndDate
                    AND b.CheckOut > @SelectedStartDate;
                ";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedStartDate", SelectedStartDate);
                        command.Parameters.AddWithValue("@SelectedEndDate", SelectedEndDate);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                roomIds.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi khi lấy phòng đã đặt: " + ex.Message);
                }
                finally
                {
                    DatabaseConnector.Close(connection);
                }
            }

            return roomIds;
        }
    }
}
