using System;
using System.Collections.Generic;

namespace Entities
{
    public class BookingRoom
    {
        public int BookingRoomID { get; set; }
        public int BookingID { get; set; }
        public int RoomID { get; set; }

        public BookingRoom(int bookingId, int roomId)
        {
            BookingID = bookingId;
            RoomID = roomId;
        }

        public BookingRoom(int bookingRoomId, int bookingId, int roomId)
        {
            BookingRoomID = bookingRoomId;
            BookingID = bookingId;
            RoomID = roomId;
        }

        public override bool Equals(object obj)
        {
            return obj is BookingRoom room &&
                   BookingRoomID == room.BookingRoomID;
        }

        public override int GetHashCode()
        {
            return 1292660469 + BookingRoomID.GetHashCode();
        }
    }
}
