using System;

namespace Entities
{
    public class StayPeriod
    {
        // Private fields
        private int _stayPeriodID;
        private int _bookingID;
        private DateTime _checkinActual;
        private DateTime _checkoutActual;

        // Constructor đầy đủ
        public StayPeriod(int stayPeriodID, int bookingID, int guestID, DateTime checkinActual, DateTime checkoutActual)
        {
            _stayPeriodID = stayPeriodID;
            _bookingID = bookingID;
            
            _checkinActual = checkinActual;
            _checkoutActual = checkoutActual;
        }

        // Constructor rút gọn
        public StayPeriod(int bookingID, DateTime checkinActual)
        {
            _bookingID = bookingID;
            _checkinActual = checkinActual;
        }

        // Public properties
        public int StayPeriodID
        {
            get { return _stayPeriodID; }
            set { _stayPeriodID = value; }
        }

        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        public DateTime CheckinActual
        {
            get { return _checkinActual; }
            set { _checkinActual = value; }
        }

        public DateTime CheckoutActual
        {
            get { return _checkoutActual; }
            set { _checkoutActual = value; }
        }

        // Nếu bạn muốn, có thể thêm property GuestID:
        /*
        public int GuestID
        {
            get { return _guestID; }
            set { _guestID = value; }
        }
        */
    }
}
