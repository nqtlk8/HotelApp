public class InvoiceItemView
{
    // Private fields
    private string _type;
    private string _name;
    private double _unitPrice;
    private int _quantity;

    // Constructor mặc định
    public InvoiceItemView()
    {
    }

    // Constructor đầy đủ
    public InvoiceItemView(string type, string name, double unitPrice, int quantity)
    {
        _type = type;
        _name = name;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }

    // Public properties
    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public double UnitPrice
    {
        get { return _unitPrice; }
        set { _unitPrice = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    // Readonly property
    public double Total
    {
        get { return _unitPrice * _quantity; }
    }
}
