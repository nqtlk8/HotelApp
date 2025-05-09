using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HistoryServicePrice
    {
        public string Service { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime EffectiveDate { get; set; }
        public HistoryServicePrice(string service, decimal servicePrice, DateTime effectiveDate)
        {
            Service = service;
            ServicePrice = servicePrice;
            EffectiveDate = effectiveDate;
        }

        public override bool Equals(object obj)
        {
            return obj is HistoryServicePrice price &&
                   Service == price.Service &&
                   ServicePrice == price.ServicePrice;
        }

        public override int GetHashCode()
        {
            int hashCode = -840931435;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Service);
            hashCode = hashCode * -1521134295 + ServicePrice.GetHashCode();
            return hashCode;
        }
    }
}
