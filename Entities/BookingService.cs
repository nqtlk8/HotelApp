using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookingService
    {
        public int BookingServiceID { get; set; }
        public int BookingID { get; set; }
        public int ServiceID { get; set; }
        public int Quantity { get; set; }
        public DateTime UsedDate { get; set; }

        public BookingService(int bookingServiceID, int bookingID, int serviceID, int quantity, DateTime usedDate)
        {
            BookingServiceID = bookingServiceID;
            BookingID = bookingID;
            ServiceID = serviceID;
            Quantity = quantity;
            UsedDate = usedDate;
        }
    }
}