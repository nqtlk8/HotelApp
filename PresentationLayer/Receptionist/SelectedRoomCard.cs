using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Receptionist
{
    public static class SelectedRoomCard
    {
        public static List<int> SelectedRoomCards ;
        public static List<int> GetSelectedRoomCards(int roomid)
        {
            if (SelectedRoomCards.Contains(roomid))
            {
                SelectedRoomCards.Remove(roomid);
            }
            else
            {
                SelectedRoomCards.Add(roomid);
            }
            return SelectedRoomCards;
        }
    }
}
