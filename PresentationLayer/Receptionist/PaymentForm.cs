using BusinessLogicLayer;
using Entities;
using NLog;
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
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
        }

        private async void LoadBookingsWithStayPeriod()
        {
            try
            {
                List<Booking> bookings = await BookingBLL.GetBookingsWithStayPeriodAsync();

                // Gán dữ liệu vào DataGridView
                dgvBookings.DataSource = bookings;

                // Tùy chọn định dạng cột nếu muốn
                dgvBookings.Columns["BookingID"].HeaderText = "Mã Đặt Phòng";
                dgvBookings.Columns["GuestID"].HeaderText = "Mã Khách";
                dgvBookings.Columns["FullName"].HeaderText = "Tên Khách";
                dgvBookings.Columns["CheckInDate"].HeaderText = "Ngày Nhận";
                dgvBookings.Columns["CheckOutDate"].HeaderText = "Ngày Trả";
                dgvBookings.Columns["TotalPrice"].HeaderText = "Tổng Tiền";

                dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải danh sách booking có stay period: " + ex.Message);
            }
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            LoadBookingsWithStayPeriod();

        }

        private async void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra có dòng nào được chọn chưa
            if (dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng booking.");
                return;
            }

            var selectedRow = dgvBookings.SelectedRows[0];

            // Kiểm tra dòng có đủ cột hay không
            if (selectedRow.Cells.Count == 0)
            {
                MessageBox.Show("Dòng được chọn không có dữ liệu.");
                return;
            }

            // Tìm cột BookingID trong DataGridView
            int bookingIdColumnIndex = -1;
            foreach (DataGridViewColumn col in dgvBookings.Columns)
            {
                if (col.Name.Equals("BookingID", StringComparison.OrdinalIgnoreCase))
                {
                    bookingIdColumnIndex = col.Index;
                    break;
                }
            }

            if (bookingIdColumnIndex == -1)
            {
                MessageBox.Show("Không tìm thấy cột BookingID trong bảng.");
                return;
            }

            var bookingIdValue = selectedRow.Cells[bookingIdColumnIndex].Value;

            if (bookingIdValue == null || bookingIdValue == DBNull.Value)
            {
                MessageBox.Show("Giá trị BookingID không hợp lệ.");
                return;
            }

            if (!int.TryParse(bookingIdValue.ToString(), out int bookingId))
            {
                MessageBox.Show("Giá trị BookingID không phải số hợp lệ.");
                return;
            }

            try
            {
                // Gọi hàm tạo hóa đơn từ BookingID
                var invoice = await InvoiceBLL.CreateInvoiceForBooking(bookingId);

                if (invoice == null)
                {
                    MessageBox.Show("Không thể tạo hóa đơn cho booking này.");
                    return;
                }

               // logger.Info($"Tạo hóa đơn thành công cho BookingID: {bookingId}, InvoiceID: {invoice.InvoiceID}");
                MessageBox.Show($"Tạo hóa đơn thành công. InvoiceID: {invoice.InvoiceID}");
                // Mở form Invoice truyền InvoiceID
                InvoiceForm invoiceForm = new InvoiceForm(invoice.InvoiceID);
                invoiceForm.Show();
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Lỗi khi tạo hóa đơn");
                MessageBox.Show("Đã xảy ra lỗi khi tạo hóa đơn: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}