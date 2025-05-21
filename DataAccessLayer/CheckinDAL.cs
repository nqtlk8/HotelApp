using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public static class CheckinDAL
    {
        public static async Task<bool> Checkin(StayPeriod stayPeriod, List<int> guestIds)
        {
            string insertStayPeriodQuery = @"
                INSERT INTO StayPeriod (BookingID, CheckinActual) 
                VALUES (@BookingID, @CheckinActual);
                SELECT last_insert_rowid();";

            string insertDetailQuery = "INSERT INTO StayPeriodDetail (StayPeriodID, GuestID) VALUES (@StayPeriodID, @GuestID)";

            using (var connection = await DatabaseConnector.ConnectAsync())
            {
                if (connection == null) return false;

                try
                {
                    // Thêm StayPeriod và lấy StayPeriodID mới
                    long stayPeriodId;
                    using (var command = new SQLiteCommand(insertStayPeriodQuery, connection))
                    {
                        command.Parameters.AddWithValue("@BookingID", stayPeriod.BookingID);
                        command.Parameters.AddWithValue("@CheckinActual", stayPeriod.CheckinActual.ToString("yyyy-MM-dd"));
                        stayPeriodId = (long)await command.ExecuteScalarAsync();
                    }

                    // Thêm từng guest vào StayPeriodDetail
                    foreach (int guestId in guestIds)
                    {
                        using (var command = new SQLiteCommand(insertDetailQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StayPeriodID", stayPeriodId);
                            command.Parameters.AddWithValue("@GuestID", guestId);
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("❌ Lỗi khi check-in: " + ex.Message);
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
