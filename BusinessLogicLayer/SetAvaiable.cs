using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogicLayer
{
    public static class SetAvaiable
    {
        public static async Task<List<RoomCard>> SetAvailable(List<RoomCard> rooms,DateTime starttime, DateTime endtime)
        {
            List<int> bookedRoomIds = await DataAccessLayer.GetBookingByDateDAL.GetBookedRoom(starttime, endtime);


            foreach (var room in rooms)
            {
                // Nếu có danh sách đặt phòng và RoomID hiện tại có trong danh sách đó => không khả dụng
                if (bookedRoomIds != null && bookedRoomIds.Contains(room.RoomID))
                {
                    room.isAvailable = false;
                }
                else
                {
                    room.isAvailable = true;
                }
            }

            return rooms;
        }
    }
}
