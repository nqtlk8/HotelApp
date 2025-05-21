using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InvoiceServiceDetail
    {
        public int InvoiceServiceDetailID { get; set; }
        public int InvoiceID { get; set; }
        public int ServiceID { get; set; }
        public double ServicePrice { get; set; }
        public int Quantity { get; set; }

        public InvoiceServiceDetail(int invoiceServiceDetailID, int invoiceID, int serviceID, double servicePrice, int quantity)
        {
            InvoiceServiceDetailID = invoiceServiceDetailID;
            InvoiceID = invoiceID;
            ServiceID = serviceID;
            ServicePrice = servicePrice;
            Quantity = quantity;
        }
        public InvoiceServiceDetail()
        {

        }
    }
}
