using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class FilterRoomTypeBLL
    {
        public static List<RoomCard> FilterByRoomType(List<RoomCard> roomCards, List<string> roomtype)
        {
            if (roomtype.Count == 0)
            {
                // Nếu không có loại phòng nào được chọn, hiển thị tất cả các phòng
                foreach (var room in roomCards)
                {
                    room.Visible = true;
                }
            }
            else
            {
                // Nếu có loại phòng được chọn, chỉ hiển thị các phòng thuộc loại đó
                foreach (var room in roomCards)
                {
                    if (roomtype.Contains(room.RoomType))
                    {
                        room.Visible = true;
                    }
                    else
                    {
                        room.Visible = false;
                    }
                }
            }

            return roomCards;
        }
    }
}
