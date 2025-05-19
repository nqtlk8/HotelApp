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
        public int GuestID { get; set; }
        public DateTime  CheckinActual { get; set; }
        public DateTime CheckoutActual { get; set; } 


    }
}
