using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StayPeriod
    {
        public string BookingID { get; set; }
        public string GuestID { get; set; }
        public StayPeriod(string BookingID, string GuestID)
        {
            this.BookingID = BookingID;
            this.GuestID = GuestID;
        }

        public override bool Equals(object obj)
        {
            return obj is StayPeriod period &&
                   BookingID == period.BookingID &&
                   GuestID == period.GuestID;
        }

        public override int GetHashCode()
        {
            int hashCode = 601006035;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BookingID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GuestID);
            return hashCode;
        }
    }
}
