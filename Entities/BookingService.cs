using System;

namespace Entities
{
    public class BookingService
    {
        // Private fields
        private int _bookingServiceID;
        private int _bookingID;
        private int _serviceID;
        private int _quantity;
        private DateTime _usedDate;

        // Constructor đầy đủ
        public BookingService(int bookingServiceID, int bookingID, int serviceID, int quantity, DateTime usedDate)
        {
            _bookingServiceID = bookingServiceID;
            _bookingID = bookingID;
            _serviceID = serviceID;
            _quantity = quantity;
            _usedDate = usedDate;
        }

        // Constructor mặc định
        public BookingService()
        {
        }

        // Public properties
        public int BookingServiceID
        {
            get { return _bookingServiceID; }
            set { _bookingServiceID = value; }
        }

        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        public int ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public DateTime UsedDate
        {
            get { return _usedDate; }
            set { _usedDate = value; }
        }
    }
}
