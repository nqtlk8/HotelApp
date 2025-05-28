using System;
using System.Collections.Generic;

namespace Entities
{
    public class BookingRoom
    {
        // Private fields
        private int _bookingRoomID;
        private int _bookingID;
        private int _roomID;

        // Constructor không có BookingRoomID
        public BookingRoom(int bookingId, int roomId)
        {
            _bookingID = bookingId;
            _roomID = roomId;
        }

        // Constructor đầy đủ có BookingRoomID
        public BookingRoom(int bookingRoomId, int bookingId, int roomId)
        {
            _bookingRoomID = bookingRoomId;
            _bookingID = bookingId;
            _roomID = roomId;
        }

        // Public properties
        public int BookingRoomID
        {
            get { return _bookingRoomID; }
            set { _bookingRoomID = value; }
        }

        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        public int RoomID
        {
            get { return _roomID; }
            set { _roomID = value; }
        }
    }
}
