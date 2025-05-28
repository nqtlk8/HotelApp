using System;

namespace Entities
{
    public class InvoiceRoomDetail
    {
        // Private fields
        private int _invoiceRoomDetailID;
        private int _invoiceID;
        private int _roomID;
        private double _roomPrice;
        private int _nights;

        // Constructor đầy đủ
        public InvoiceRoomDetail(int invoiceRoomDetailID, int invoiceID, int roomID, double roomPrice, int nights)
        {
            _invoiceRoomDetailID = invoiceRoomDetailID;
            _invoiceID = invoiceID;
            _roomID = roomID;
            _roomPrice = roomPrice;
            _nights = nights;
        }

        // Constructor mặc định
        public InvoiceRoomDetail()
        {
        }

        // Public properties
        public int InvoiceRoomDetailID
        {
            get { return _invoiceRoomDetailID; }
            set { _invoiceRoomDetailID = value; }
        }

        public int InvoiceID
        {
            get { return _invoiceID; }
            set { _invoiceID = value; }
        }

        public int RoomID
        {
            get { return _roomID; }
            set { _roomID = value; }
        }

        public double RoomPrice
        {
            get { return _roomPrice; }
            set { _roomPrice = value; }
        }

        public int Nights
        {
            get { return _nights; }
            set { _nights = value; }
        }
    }
}
