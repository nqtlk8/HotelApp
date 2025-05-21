using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StayPeriod
    {
        public int StayPeriodID { get; set; }
        public int BookingID { get; set; }
        public DateTime  CheckinActual { get; set; }
        public DateTime CheckoutActual { get; set; }

        public StayPeriod(int stayPeriodID, int bookingID, int guestID, DateTime checkinActual, DateTime checkoutActual)
        {
            StayPeriodID = stayPeriodID;
            BookingID = bookingID;
            CheckinActual = checkinActual;
            CheckoutActual = checkoutActual;
        }

        public StayPeriod(int bookingID, DateTime checkinActual)
        {
            BookingID = bookingID;
            CheckinActual = checkinActual;
        }

        public override bool Equals(object obj)
        {
            return obj is StayPeriod period &&
                   StayPeriodID == period.StayPeriodID;
        }

        public override int GetHashCode()
        {
            return -1289241516 + StayPeriodID.GetHashCode();
        }
    }
}
