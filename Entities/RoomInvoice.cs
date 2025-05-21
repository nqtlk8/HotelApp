using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal class RoomInvoice
    {
        public int RoomInvoiceId { get; set; }
        public string BookingID { get; set; }
        public string RoomId { get; set; }
        public decimal RoomPrice { get; set; } 
        public DateTime Date { get; set; }
        public RoomInvoice(string bookingID, string roomId, decimal roomPrice, DateTime date)
        {
            BookingID = bookingID;
            RoomId = roomId;
            RoomPrice = roomPrice;
            Date = date;
        }

        public override bool Equals(object obj)
        {
            return obj is RoomInvoice invoice &&
                   RoomInvoiceId == invoice.RoomInvoiceId;
        }

        public override int GetHashCode()
        {
            return 421181180 + RoomInvoiceId.GetHashCode();
        }
    }

}
