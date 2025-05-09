using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookingRoom
    {
        public string BookingID { get; set; }
        public string RoomID { get; set; }
        public DateTime Date { get; set; }

        public BookingRoom(string bookingID, string roomID, DateTime date)
        {
            BookingID = bookingID;
            RoomID = roomID;
            Date = date;
        }

        public override bool Equals(object obj)
        {
            return obj is BookingRoom room &&
                   BookingID == room.BookingID;
        }

        public override int GetHashCode()
        {
            return 1292660469 + EqualityComparer<string>.Default.GetHashCode(BookingID);
        }
    }
}
