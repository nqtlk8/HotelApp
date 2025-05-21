using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int GuestID { get; set; }
        public string FullName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Double TotalPrice { get; set; }
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
