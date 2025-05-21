public class InvoiceItemView
{
    
        public string Type { get; set; }    // "Phòng" hoặc "Dịch vụ"
        public string Name { get; set; }    // Tên phòng hoặc dịch vụ
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total => UnitPrice * Quantity;
    

}
