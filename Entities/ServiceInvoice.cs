using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServiceInvoice
    {
        public string ServiceInvoiceID { get; set; }
        public decimal ServicePrice { get; set; }
        public string Service { get; set; }
        public DateTime Date { get; set; }
        public string GuestID { get; set; }
        public string BookingID { get; set; }
        public ServiceInvoice( decimal servicePrice, string service, DateTime date, string guestID, string bookingID)
        {
            ServicePrice = servicePrice;
            Service = service;
            Date = date;
            GuestID = guestID;
            BookingID = bookingID;
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceInvoice invoice &&
                   ServiceInvoiceID == invoice.ServiceInvoiceID;
        }

        public override int GetHashCode()
        {
            return 574245874 + EqualityComparer<string>.Default.GetHashCode(ServiceInvoiceID);
        }
    }
}
