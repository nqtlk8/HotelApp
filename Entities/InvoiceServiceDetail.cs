using System;

namespace Entities
{
    public class InvoiceServiceDetail
    {
        // Private fields
        private int _invoiceServiceDetailID;
        private int _invoiceID;
        private int _serviceID;
        private double _servicePrice;
        private int _quantity;

        // Constructor đầy đủ
        public InvoiceServiceDetail(int invoiceServiceDetailID, int invoiceID, int serviceID, double servicePrice, int quantity)
        {
            _invoiceServiceDetailID = invoiceServiceDetailID;
            _invoiceID = invoiceID;
            _serviceID = serviceID;
            _servicePrice = servicePrice;
            _quantity = quantity;
        }

        // Constructor mặc định
        public InvoiceServiceDetail()
        {
        }

        // Public properties
        public int InvoiceServiceDetailID
        {
            get { return _invoiceServiceDetailID; }
            set { _invoiceServiceDetailID = value; }
        }

        public int InvoiceID
        {
            get { return _invoiceID; }
            set { _invoiceID = value; }
        }

        public int ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

        public double ServicePrice
        {
            get { return _servicePrice; }
            set { _servicePrice = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
    }
}
