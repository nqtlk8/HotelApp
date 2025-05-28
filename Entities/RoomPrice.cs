using System;

namespace Entities
{
    public class RoomPrice
    {
        // Private fields
        private int _priceID;
        private int _roomID;
        private double _price;
        private DateTime _startDate;
        private DateTime _endDate;

        // Constructor mặc định
        public RoomPrice()
        {
        }

        // Constructor đầy đủ
        public RoomPrice(int priceID, int roomID, double price, DateTime startDate, DateTime endDate)
        {
            _priceID = priceID;
            _roomID = roomID;
            _price = price;
            _startDate = startDate;
            _endDate = endDate;
        }

        // Public properties
        public int PriceID
        {
            get { return _priceID; }
            set { _priceID = value; }
        }

        public int RoomID
        {
            get { return _roomID; }
            set { _roomID = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
    }
}
