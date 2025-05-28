using System;

namespace Entities
{
    public class RoomInfo
    {
        // Private fields
        private int _roomID;
        private string _roomType;
        private int _capacity;
        private string _description;
        private string _status;

        // Constructor mặc định
        public RoomInfo()
        {
        }

        // Constructor đầy đủ (không có status)
        public RoomInfo(int roomID, string roomType, int capacity, string description)
        {
            _roomID = roomID;
            _roomType = roomType;
            _capacity = capacity;
            _description = description;
        }

        // Public properties
        public int RoomID
        {
            get { return _roomID; }
            set { _roomID = value; }
        }

        public string RoomType
        {
            get { return _roomType; }
            set { _roomType = value; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
