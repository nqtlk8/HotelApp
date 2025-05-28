using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booking
    {
        // Private fields
        private int _bookingID;
        private int _guestID;
        private string _fullName;
        private DateTime _checkInDate;
        private DateTime _checkOutDate;
        private double _totalPrice;

        // Constructor
        

        // Public properties
        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        public int GuestID
        {
            get { return _guestID; }
            set { _guestID = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public DateTime CheckInDate
        {
            get { return _checkInDate; }
            set { _checkInDate = value; }
        }

        public DateTime CheckOutDate
        {
            get { return _checkOutDate; }
            set { _checkOutDate = value; }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public Booking(int guestID, string fullname,DateTime checkInDate, DateTime checkOutDate, Double totalPrice)
        {
            GuestID = guestID;
            FullName = fullname;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }
        public Booking(int bookingid, int guestID, string fullname, DateTime checkInDate, DateTime checkOutDate, Double totalPrice)
        {
            BookingID = bookingid;
            GuestID = guestID;
            FullName = fullname;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
        }

        public override bool Equals(object obj)
        {
            return obj is Booking booking &&
                   BookingID == booking.BookingID;
        }

        public override int GetHashCode()
        {
            return 1292660469 + BookingID.GetHashCode();
        }
    }
}
