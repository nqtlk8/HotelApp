using BusinessLogicLayer;
using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Receptionist
{
    public partial class BookingServiceForm : Form
    {

        private int currentBookingID;
        private DateTime usedDate;


        private Dictionary<int, int> selectedServices = new Dictionary<int, int>();
        private int previousSelectedRowIndex = -1;  // Biến kiểm tra dòng đã chọn

        public BookingServiceForm()
        {
            InitializeComponent();
        }
        private List<BookingService> selectedBookingServices = new List<BookingService>();

        private async void BookingService_Load(object sender, EventArgs e)
        {
            await LoadBookingsToGridAsync();
        }

        private async Task LoadBookingsToGridAsync()
        {
            var bookings = await BookingBLL.GetBooking();
            dgvBooking.DataSource = bookings ?? new List<Booking>();

            // Ẩn và đổi tên cột
            dgvBooking.Columns["GuestID"].Visible = false;
            dgvBooking.Columns["FullName"].HeaderText = "Họ và tên khách";
            dgvBooking.Columns["CheckInDate"].HeaderText = "Ngày nhận phòng";
            dgvBooking.Columns["CheckOutDate"].HeaderText = "Ngày trả phòng";
            dgvBooking.Columns["TotalPrice"].HeaderText = "Tổng tiền";
        }

        private async void dgvBooking_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooking.CurrentRow != null)
            {
                usedDate = Convert.ToDateTime(dgvBooking.CurrentRow.Cells["CheckInDate"].Value);

                currentBookingID = Convert.ToInt32(dgvBooking.CurrentRow.Cells["BookingID"].Value);
                int currentIndex = dgvBooking.CurrentRow.Index;
                if (currentIndex == previousSelectedRowIndex) return; // Không gọi lại nếu dòng không đổi
                previousSelectedRowIndex = currentIndex;

                DateTime checkInDate = Convert.ToDateTime(dgvBooking.CurrentRow.Cells["CheckInDate"].Value);
                await LoadServicesForDate(checkInDate);

                // ✳️ Hiển thị thông tin ra TextBox
                string info = $"🔖 Mã Booking: {dgvBooking.CurrentRow.Cells["BookingID"].Value}\r\n" +
                              $"👤 Khách: {dgvBooking.CurrentRow.Cells["FullName"].Value}\r\n" +
                              $"📅 Ngày nhận: {checkInDate:dd/MM/yyyy}\r\n" +
                              $"📅 Ngày trả: {Convert.ToDateTime(dgvBooking.CurrentRow.Cells["CheckOutDate"].Value):dd/MM/yyyy}\r\n" +
                              $"💰 Tổng tiền: {Convert.ToDouble(dgvBooking.CurrentRow.Cells["TotalPrice"].Value):C0}";

                txtBookingInfo.Text = info;

            }
        }

        private async Task LoadServicesForDate(DateTime checkInDate)
        {
            var services = await ServiceDAL.GetAllServices();

            var distinctServices = services
                .GroupBy(s => s.ServiceID)
                .Select(g => g.First())
                .ToList();

            flowLayoutPanelServices.Controls.Clear();
            selectedServices.Clear();

            foreach (var service in distinctServices)
            {
                var priceList = await ServicePriceDAL.GetPricesByServiceIDAsync(service.ServiceID);

                var currentPrice = priceList
                    .Where(p => checkInDate >= p.StartDate && checkInDate <= p.EndDate)
                    .OrderByDescending(p => p.StartDate)
                    .FirstOrDefault();

                double price = currentPrice?.ServicePriceValue ?? 0;

                var panel = new Panel
                {
                    Width = 100,
                    Height = 100,
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = service.ServiceID,
                    BackColor = Color.White
                };

                var label = new Label
                {
                    Name = "lblServiceText",
                    Text = $"{service.ServiceName}\n{service.Descrip}\nGiá: {price:C0}\nSố lượng: 0",
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                panel.Controls.Add(label);
                panel.Click += ServicePanel_Click;
                label.Click += ServicePanel_Click;

                flowLayoutPanelServices.Controls.Add(panel);
            }

            flowLayoutPanelServices.AutoScroll = true;
            flowLayoutPanelServices.WrapContents = true;
        }

        private void ServicePanel_Click(object sender, EventArgs e)
        {
            Control clicked = sender as Control;
            Panel panel = clicked is Panel ? (Panel)clicked : (Panel)clicked.Parent;
            int serviceID = (int)panel.Tag;
            Label label = panel.Controls.Find("lblServiceText", true).FirstOrDefault() as Label;

            if (!selectedServices.ContainsKey(serviceID))
            {
                selectedServices[serviceID] = 1;
            }
            else
            {
                selectedServices[serviceID]++;
            }

            int quantity = selectedServices[serviceID];

            var textLines = label.Text.Split('\n');
            if (textLines.Length >= 4)
            {
                textLines[3] = $"Số lượng: {quantity}";
                label.Text = string.Join("\n", textLines);
            }

            panel.BackColor = quantity > 0 ? Color.LightGreen : Color.White;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (currentBookingID == -1)
            {
                MessageBox.Show("Vui lòng chọn booking trước.");
                return;
            }

            if (selectedServices.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dịch vụ.");
                return;
            }

            foreach (var kvp in selectedServices)
            {
                int serviceID = kvp.Key;
                int quantity = kvp.Value;

                if (quantity <= 0) continue;

                var existing = selectedBookingServices.FirstOrDefault(s =>
                    s.ServiceID == serviceID && s.UsedDate == usedDate);

                if (existing != null)
                {
                    existing.Quantity += quantity;
                }
                else
                {
                    selectedBookingServices.Add(new BookingService
                    {
                        BookingID = currentBookingID,
                        ServiceID = serviceID,
                        Quantity = quantity,
                        UsedDate = usedDate
                    });
                }
            }

            UpdateSelectedServicesTextBox();
            MessageBox.Show("✅ Dịch vụ đã được thêm.");
        }

        private void UpdateSelectedServicesTextBox()
        {
            var sb = new StringBuilder();
            sb.AppendLine("🔔 Dịch vụ đã chọn:");

            foreach (var item in selectedBookingServices)
            {
                sb.AppendLine($"📦 BookingID: {item.BookingID} | ServiceID: {item.ServiceID} | Số lượng: {item.Quantity} | Ngày dùng: {item.UsedDate:dd/MM/yyyy}");
            }

            txtSelectedServicesInfo.Text = sb.ToString();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            bool result = await BookingServiceBLL.AddMultipleBookingServicesAsync(selectedBookingServices);

            if (result)
            {
                MessageBox.Show("✅ Tất cả dịch vụ đã được lưu vào cơ sở dữ liệu!");
                selectedBookingServices.Clear();  // Xoá danh sách sau khi lưu nếu cần
                txtSelectedServicesInfo.Clear();      // Cập nhật UI
                                                      // Reset tất cả panel
                foreach (Panel panel in flowLayoutPanelServices.Controls.OfType<Panel>())
                {
                    panel.BackColor = Color.White;

                    Label label = panel.Controls.Find("lblServiceText", true).FirstOrDefault() as Label;
                    if (label != null)
                    {
                        var textLines = label.Text.Split('\n');
                        if (textLines.Length >= 4)
                        {
                            textLines[3] = "Số lượng: 0";
                            label.Text = string.Join("\n", textLines);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("❌ Một số dịch vụ không thể lưu được.");
            }
            
        }

        private void flowLayoutPanelServices_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

