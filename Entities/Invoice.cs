using System;

namespace Entities
{
    public class Invoice
    {
        // Private fields
        private int _invoiceID;
        private int _bookingID;
        private DateTime _invoiceDate;
        private double _roomTotal;
        private double _serviceTotal;
        private double _vat;
        private double _surcharge;
        private double _totalPayment;

        // Constructor mặc định
        public Invoice()
        {
        }

        // Constructor đầy đủ
        public Invoice(int invoiceID, int bookingID, DateTime invoiceDate,
                       double roomTotal, double serviceTotal, double vat,
                       double surcharge, double totalPayment)
        {
            _invoiceID = invoiceID;
            _bookingID = bookingID;
            _invoiceDate = invoiceDate;
            _roomTotal = roomTotal;
            _serviceTotal = serviceTotal;
            _vat = vat;
            _surcharge = surcharge;
            _totalPayment = totalPayment;
        }

        // Public properties
        public int InvoiceID
        {
            get { return _invoiceID; }
            set { _invoiceID = value; }
        }

        public int BookingID
        {
            get { return _bookingID; }
            set { _bookingID = value; }
        }

        public DateTime InvoiceDate
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }

        public double RoomTotal
        {
            get { return _roomTotal; }
            set { _roomTotal = value; }
        }

        public double ServiceTotal
        {
            get { return _serviceTotal; }
            set { _serviceTotal = value; }
        }

        public double VAT
        {
            get { return _vat; }
            set { _vat = value; }
        }

        public double Surcharge
        {
            get { return _surcharge; }
            set { _surcharge = value; }
        }

        public double TotalPayment
        {
            get { return _totalPayment; }
            set { _totalPayment = value; }
        }
    }
}
