using System;

namespace Entities
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double RoomTotal { get; set; }       // Tổng tiền phòng
        public double ServiceTotal { get; set; }    // Tổng tiền dịch vụ
        public double VAT { get; set; }             // Thuế VAT (10%)
        public double Surcharge { get; set; }       // Phụ thu
        public double TotalPayment { get; set; }    // Tổng tiền cần thanh toán

        public Invoice() { }

        public Invoice(int invoiceID, int bookingID, DateTime invoiceDate,
               double roomTotal, double serviceTotal, double vat,
               double surcharge, double totalPayment)
        {
            InvoiceID = invoiceID;
            BookingID = bookingID;
            InvoiceDate = invoiceDate;
            RoomTotal = roomTotal;
            ServiceTotal = serviceTotal;
            VAT = vat;
            Surcharge = surcharge;
            TotalPayment = totalPayment;
        }

    }
}
