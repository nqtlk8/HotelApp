using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InvoiceRoomDetail
    {
        public int InvoiceRoomDetailID { get; set; }
        public int InvoiceID { get; set; }
        public int RoomID { get; set; }
        public double RoomPrice { get; set; }
        public int Nights { get; set; }

        public InvoiceRoomDetail(int invoiceRoomDetailID, int invoiceID, int roomID, double roomPrice, int nights)
        {
            InvoiceRoomDetailID = invoiceRoomDetailID;
            InvoiceID = invoiceID;
            RoomID = roomID;
            RoomPrice = roomPrice;
            Nights = nights;
        }
        public InvoiceRoomDetail()
        {

        }
    }
}
