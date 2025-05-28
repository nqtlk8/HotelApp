using BusinessLogicLayer;
using Entities;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class InvoiceForm : Form
    {
        private int invoiceID;  // Biến lưu ID hóa đơn đang xem
        public int bookingID;

        // Các label hiển thị tổng tiền, VAT, phụ thu, tổng thanh toán, số tiền bằng chữ
        private Label lblTotalText, lblTotal;
        private Label lblVATText, lblVAT;
        private Label lblLateFeeText, lblLateFee;
        private Label lblFinalTotalText;
        private Label lblAmountInWordsText, lblAmountInWords;
        private Label lblUser, lblUserName;
        private Label lblGuest, lblGuestName;
        private Label lblInvoiceIDText, lblInvoiceID;
        private Label lblInvoiceDateText, lblInvoiceDate;
        private Label  lblSurcharge, lblTotalPayment;

     
        public InvoiceForm(int selectedInvoiceId)
        {
            InitializeComponent();     // Bắt buộc gọi đầu tiên, để tạo các control

            invoiceID = selectedInvoiceId;  // Gán biến cần dùng

            DisplayInfo();             // Hiển thị các thông tin cơ bản (nên chắc chắn control đã sẵn sàng)

            InitializeControls();      // Khởi tạo các control tùy chỉnh (nếu có)

            AddColumnsToGrid();        // Thêm cột cho DataGridView hoặc tương tự

            LoadData();                // Load dữ liệu chung (ví dụ các danh sách, dữ liệu phụ)

            LoadInvoice();             // Load thông tin chi tiết hóa đơn (invoice)

        }


        private async void DisplayInfo()
        {
            try
            {
                var guest = await GuestBLL.GetGuestByInvoiceIDAsync(invoiceID);
                
                if (guest != null)
                {
                    lblName.Text = "Tên khách: " + guest.FullName;
                    lblEmail.Text = "Email: " + guest.Email;
                    lblSDT.Text = "SĐT: " + guest.PhoneNumber;
                    lblCCCD.Text = "CCCD: " + guest.GuestPrivateInfo;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng từ hóa đơn này.");
                }


                var booking = await BookingBLL.GetBookingByInvoiceIDAsync(invoiceID);
                if (booking != null)
                {
                    lblBookingID.Text = "Booking ID: " + booking.BookingID;
                    lblCheckIn.Text = "Ngày nhận phòng: " + booking.CheckInDate.ToShortDateString();
                    lblCheckOut.Text = "Ngày trả phòng: " + booking.CheckOutDate.ToShortDateString();
                    int nights = (booking.CheckOutDate - booking.CheckInDate).Days;
                    lblCount.Text = "Số đêm: " + nights;
                }
                else
                {
                    MessageBox.Show("❌ Không tìm thấy thông tin booking.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải thông tin: " + ex.Message);
            }
        }


        private async void InitializeControls()
        {
            // Khởi tạo DataGridView
            dataGridView1 = new DataGridView()
            {
                Location = new Point(20, 220),
                Width = 420,
                //Height = 700,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ScrollBars = ScrollBars.Vertical,
                RowHeadersVisible = false,
                ReadOnly = true,
            };
            Controls.Add(dataGridView1);

            // Labels hiển thị tính toán
            int leftX = 20;
            int startY = dataGridView1.Bottom + 15;
            int gapY = 30;

            lblTotalText = new Label()
            {
                Location = new Point(leftX, startY),
                Size = new Size(150, 25),
                Text = "Tổng tiền:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblTotal = new Label()
            {
                Location = new Point(leftX + 250, startY),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10),
            };

            lblVATText = new Label()
            {
                Location = new Point(leftX, startY + gapY),
                Size = new Size(150, 25),
                Text = "VAT (10%):",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblVAT = new Label()
            {
                Location = new Point(leftX + 250, startY + gapY),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10),
            };

            lblLateFeeText = new Label()
            {
                Location = new Point(leftX, startY + gapY * 2),
                Size = new Size(150, 25),
                Text = "Phụ thu:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblSurcharge = new Label()
            {
                Location = new Point(leftX + 250, startY + gapY*2),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            };

            lblFinalTotalText = new Label()
            {
                Location = new Point(leftX, startY + gapY * 3),
                Size = new Size(150, 25),
                Text = "Tổng thanh toán:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblTotalPayment = new Label()
            {
                Location = new Point(leftX +250 , startY + gapY * 3),
                Size = new Size(150, 25),
              
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10),
            };
            
            
            lblUser = new Label()
            {
                Location = new Point(70, lblTotalPayment.Bottom + 20),
                Size = new Size(150, 25),
                Text = "Lễ tân:",
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleLeft
            };
            
            lblGuest = new Label()
            {
                Location = new Point(230, lblTotalPayment.Bottom + 20),
                Size = new Size(150, 25),
                Text = "Khách hàng:",
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleRight
            };
            var guest = await GuestBLL.GetGuestByInvoiceIDAsync(invoiceID);
            lblGuestName = new Label()
            {

                Location = new Point(220, lblTotalPayment.Bottom + 90),
                Size = new Size(150, 25),
                Text = guest.FullName,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10),
            };
            
            Controls.Add(lblTotalText);
            Controls.Add(lblTotal);
            Controls.Add(lblVATText);
            Controls.Add(lblVAT);
            Controls.Add(lblLateFeeText);
            Controls.Add(lblLateFee);
            Controls.Add(lblFinalTotalText);
            Controls.Add(lblTotalPayment);
            Controls.Add(lblAmountInWordsText);
            Controls.Add(lblAmountInWords);
            Controls.Add(lblUser);
            Controls.Add(lblUserName);
            Controls.Add(lblGuest);
            Controls.Add(lblGuestName);
            Controls.Add(lblSurcharge);
            leftX = 20;
            int topY = 20;
            gapY = 30;


            // Label Mã hóa đơn
            lblInvoiceIDText = new Label()
            {
                Location = new Point(leftX, topY),
                Size = new Size(150, 25),
                Text = "Mã hóa đơn:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblInvoiceID = new Label()
            {
                Location = new Point(leftX + 150, topY),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10)
            };
           
            // Label Ngày hóa đơn
            lblInvoiceDateText = new Label()
            {
                Location = new Point(leftX, topY + gapY),
                Size = new Size(150, 25),
                Text = "Ngày hóa đơn:",
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            lblInvoiceDate = new Label()
            {
                Location = new Point(leftX + 150, topY + gapY),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10)
            };
            // Thêm vào Controls
            Controls.Add(lblInvoiceIDText);
            Controls.Add(lblInvoiceID);
            Controls.Add(lblInvoiceDateText);
            Controls.Add(lblInvoiceDate);
        }

        private void AddColumnsToGrid()
        {
            dataGridView1.Columns.Add("Name", "Tên");
            dataGridView1.Columns.Add("Type", "Loại");
            dataGridView1.Columns.Add("Quantity", "SL");
            dataGridView1.Columns.Add("Price", "Đơn giá");
            dataGridView1.Columns.Add("Total", "Thành tiền");
        }

        private async void LoadData()
        {
            try
            {
                dataGridView1.Rows.Clear();

                // Lấy dữ liệu từ BLL
                var items = await InvoiceBLL.GetInvoiceItemsForDisplay(invoiceID); // bookingID hoặc invoiceID

                foreach (var item in items)
                {
                    dataGridView1.Rows.Add(item.Name, item.Type, item.Quantity, item.UnitPrice.ToString("N0"), item.Total.ToString("N0"));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private async void LoadInvoice()
        {
            var invoice = await BusinessLogicLayer.InvoiceBLL.GetInvoiceByIdAsync(invoiceID);
            MessageBox.Show(invoiceID.ToString());

            if (invoice == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn.");
                return;
            }

            // Nếu đến đây, chắc chắn invoice != null
            lblInvoiceID.Text = invoice.InvoiceID.ToString();
            lblInvoiceDate.Text = invoice.InvoiceDate.ToString("dd/MM/yyyy");
            lblTotal.Text = (invoice.RoomTotal + invoice.ServiceTotal.ToString("N0") ?? "0") +" VNĐ";
            lblVAT.Text = invoice.VAT.ToString("N0") + " VNĐ";
            lblSurcharge.Text = invoice.Surcharge.ToString("N0") + " VNĐ";
            lblTotalPayment.Text = invoice.TotalPayment.ToString("N0") + " VNĐ";

           
        }

    
    }
}
