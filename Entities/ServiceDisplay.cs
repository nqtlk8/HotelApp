using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServiceDisplay
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Descrip { get; set; }
        public double ServicePriceValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
