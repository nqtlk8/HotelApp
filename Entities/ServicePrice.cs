using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServicePrice
    {
        public int ServicePriceID { get; set; }
        public int ServiceID { get; set; }
        public double ServicePriceValue { get; set; } // Renamed to avoid name clash with class

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
