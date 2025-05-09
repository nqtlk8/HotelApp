using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booking
    {
        public string BookingID { get; set; }
        public string GuestID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Booking(string guestID, DateTime checkInDate, DateTime checkOutDate, decimal totalPrice)
        {
            GuestID = guestID;
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
            return -2085779058 + EqualityComparer<string>.Default.GetHashCode(BookingID);
        }
    }
}
