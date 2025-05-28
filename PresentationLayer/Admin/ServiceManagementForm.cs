using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PresentationLayer.Admin
{
    public partial class ServiceManagementForm : Form
    {
        public ServiceManagementForm()
        {
            InitializeComponent();
            this.TopLevel = false;       // Cho phép form này được nhúng
            this.FormBorderStyle = FormBorderStyle.None;  // Ẩn viền
            this.Dock = DockStyle.Fill;  // Co giãn toàn Panel
            LoadData();
        }
        // Trong LoadData():
        private async void LoadData()
        {
            var data = await ServiceBLL.GetAllServices();
            dgvServices.DataSource = data;

            // Ẩn cột ServiceID
            dgvServices.Columns["ServiceID"].Visible = false;

            // Cấu hình tiêu đề các cột
            dgvServices.Columns["ServiceName"].HeaderText = "Dịch vụ";
            dgvServices.Columns["Descrip"].HeaderText = "Mô tả";
            dgvServices.Columns["IsActive"].HeaderText = "Trạng thái";

            // Xóa sự kiện trước để tránh gắn nhiều lần
            dgvServices.CellFormatting -= DgvServices_CellFormatting;
            dgvServices.CellFormatting += DgvServices_CellFormatting;

            // Không dùng Fill để có thể cuộn
            dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServices.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvServices.ScrollBars = ScrollBars.Both;
        }

        // Đặt ra ngoài để tái sử dụng
        private void DgvServices_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvServices.Columns[e.ColumnIndex].Name == "IsActive" && e.Value != null && e.Value != DBNull.Value)
            {
                try
                {
                    int isActive = Convert.ToInt32(e.Value); // an toàn hơn (int)e.Value
                    e.Value = isActive == 1 ? "Hoạt động" : "Không hoạt động";
                    e.FormattingApplied = true;
                }
                catch
                {
                    e.Value = "Không xác định";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvPrices_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvServices.RowHeadersDefaultCellStyle.ForeColor))
            {
                string stt = (e.RowIndex + 1).ToString();
                e.Graphics.DrawString(stt, dgvServices.DefaultCellStyle.Font, b,
                    e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }


        private async void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow != null)
            {
                var row = dgvServices.CurrentRow;

                txtServiceName.Text = row.Cells["ServiceName"].Value?.ToString() ?? "";
                txtDescrip.Text = row.Cells["Descrip"].Value?.ToString() ?? "";

                var isActiveValue = row.Cells["IsActive"].Value;
                if (isActiveValue != null && int.TryParse(isActiveValue.ToString(), out int isActive))
                {
                    cmbIsActive.SelectedValue = isActive;
                }
                else
                {
                    cmbIsActive.SelectedIndex = -1; // hoặc mặc định
                }

                LoadPriceData();
            }


        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            string name = txtServiceName.Text.Trim();
            string descrip = txtDescrip.Text.Trim();

            // Kiểm tra hợp lệ đầu vào
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên dịch vụ.");
                return;
            }

            if (cmbIsActive.SelectedItem == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn trạng thái hoạt động.");
                return;
            }

            int isActive = (int)((KeyValuePair<int, string>)cmbIsActive.SelectedItem).Key;



            // Gọi hàm thêm dữ liệu
            bool result = await ServiceBLL.AddService(name, descrip, isActive);

            if (result)
            {
                MessageBox.Show("✅ Đã thêm dịch vụ thành công.");

                // Tải lại dữ liệu lên DataGridView
                LoadData();

                // Xóa input
                btnClear.PerformClick();
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtServiceName.Clear();
            txtDescrip.Clear();
            cmbIsActive.SelectedIndex = -1;

            // Nếu muốn bỏ chọn dòng trên DataGridView luôn:
            dgvServices.ClearSelection();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để cập nhật.");
                return;
            }

            if (cmbIsActive.SelectedItem == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn trạng thái hoạt động.");
                return;
            }

            int isActive = (int)((KeyValuePair<int, string>)cmbIsActive.SelectedItem).Key;



            int serviceID = Convert.ToInt32(dgvServices.CurrentRow.Cells["ServiceID"].Value);  // Lấy ID từ DataGridView
            var service = new ServiceInfo
            {
                ServiceID = serviceID,
                ServiceName = txtServiceName.Text,
                Descrip = txtDescrip.Text,
                IsActive = isActive
            };

            bool success = await ServiceBLL.UpdateService(service);
            if (success)
            {
                MessageBox.Show("Cập nhật dịch vụ thành công!");
                LoadData(); // Load lại dữ liệu mới sau khi update
            }
            else
            {
                MessageBox.Show("Cập nhật dịch vụ thất bại.");
            }
        }

        private async void LoadPriceData()
        {
            if (dgvServices.CurrentRow != null)
            {
                int serviceID = Convert.ToInt32(dgvServices.CurrentRow.Cells["ServiceID"].Value);  // Lấy ID từ DataGridView


                // Load giá dịch vụ theo ServiceID
                var prices = await ServiceBLL.GetServicePricesByServiceID(serviceID);
                dgvPrices.DataSource = prices;

                // Cấu hình hiển thị cột (tuỳ chọn)
                dgvPrices.Columns["ServicePriceID"].Visible = false; // ẩn ID giá nếu muốn
                dgvPrices.Columns["ServiceID"].Visible = false;      // ẩn ServiceID nếu không cần hiển thị
                dgvPrices.Columns["ServicePriceValue"].HeaderText = "Đơn giá";
                dgvPrices.Columns["StartDate"].HeaderText = "Start";
                dgvPrices.Columns["EndDate"].HeaderText = "End";

                dgvPrices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private void dgvServices_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvServices.RowHeadersDefaultCellStyle.ForeColor))
            {
                string stt = (e.RowIndex + 1).ToString();
                e.Graphics.DrawString(stt, dgvServices.DefaultCellStyle.Font, b,
                    e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvPrices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPrices.CurrentRow != null)
            {
                var row = dgvPrices.CurrentRow;

                txtPrice.Text = row.Cells["ServicePriceValue"].Value?.ToString() ?? "";
                // Hiển thị StartDate và EndDate lên DateTimePicker
                if (row.Cells["StartDate"].Value != DBNull.Value)
                    dtpStart.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);

                if (row.Cells["EndDate"].Value != DBNull.Value)
                    dtpEnd.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
            }
        }


        private async void btnAddPrice_Click(object sender, EventArgs e)
        {
            try
            {

                // Lấy dữ liệu từ form
                int serviceID = Convert.ToInt32(dgvServices.CurrentRow.Cells["ServiceID"].Value);  // Lấy ID từ DataGridView
                if (!double.TryParse(txtPrice.Text.Trim(), out double price))
                {
                    MessageBox.Show("❌ Vui lòng nhập đúng định dạng đơn giá.");
                    return;
                }

                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date;

                // Tạo đối tượng ServicePrice
                ServicePrice newPrice = new ServicePrice
                {
                    ServiceID = serviceID,
                    ServicePriceValue = price,
                    StartDate = startDate,
                    EndDate = endDate
                };

                // Gọi BLL để thêm vào DB
                bool success = await ServicePriceBLL.AddServicePriceAsync(newPrice);
                if (success)
                {
                    MessageBox.Show("✅ Thêm giá thành công.");
                    LoadPriceData(); // Hàm load lại DataGridView
                }
                else
                {
                    MessageBox.Show("❌ Thêm giá thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi thêm giá: " + ex.Message);
            }
        }

        private void btnClearPrice_Click(object sender, EventArgs e)
        {
            txtPrice.Clear();
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            // Nếu muốn bỏ chọn dòng trên DataGridView luôn:
            dgvPrices.ClearSelection();
        }


        private async void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPrices.CurrentRow == null)
                {
                    MessageBox.Show("❗ Vui lòng chọn dòng cần cập nhật.");
                    return;
                }

                // Lấy ServicePriceID từ dòng được chọn
                int selectedIndex = dgvPrices.CurrentRow.Index;
                if (selectedIndex < 0) return;

                int serviceID = Convert.ToInt32(dgvServices.CurrentRow.Cells["ServiceID"].Value);  // Lấy ID từ DataGridView

                int servicePriceId = Convert.ToInt32(dgvPrices.CurrentRow.Cells["ServicePriceID"].Value);

                if (!double.TryParse(txtPrice.Text.Trim(), out double price))
                {
                    MessageBox.Show("❌ Đơn giá không hợp lệ.");
                    return;
                }

                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date;

                ServicePrice updatedPrice = new ServicePrice
                {
                    ServicePriceID = servicePriceId,
                    ServiceID = serviceID, // lấy từ giao diện hiện tại
                    ServicePriceValue = price,
                    StartDate = startDate,
                    EndDate = endDate
                };

                bool success = await ServicePriceBLL.UpdateServicePriceAsync(updatedPrice);
                if (success)
                {
                    MessageBox.Show("✅ Cập nhật thành công.");
                    LoadPriceData();
                }
                else
                {
                    MessageBox.Show("❌ Cập nhật thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi cập nhật giá: " + ex.Message);
            }
        }

        private void ServiceManagementForm_Load(object sender, EventArgs e)
        {
            var list = new List<KeyValuePair<int, string>>
    {
        new KeyValuePair<int, string>(1, "Hoạt động"),
        new KeyValuePair<int, string>(0, "Không hoạt động")
    };

            cmbIsActive.DataSource = new BindingSource(list, null);
            cmbIsActive.DisplayMember = "Value";
            cmbIsActive.ValueMember = "Key";
        }



        private void TimKiemGanDungTrongDGV(string tuKhoa)
        {
            foreach (DataGridViewRow row in dgvServices.Rows)
            {
                row.Visible = false; // ẩn tất cả dòng trước

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null &&
                        cell.Value.ToString().IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        row.Visible = true; // hiển thị dòng có ô phù hợp
                        break;
                    }
                }
            }
        }
        

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.Trim();
            TimKiemGanDungTrongDGV(tuKhoa);
        }
        private void LocGiaTheoNgay(DateTime ngayChon)
        {
            foreach (DataGridViewRow row in dgvPrices.Rows)
            {
                // Lấy giá trị ngày bắt đầu và kết thúc
                DateTime startDate = Convert.ToDateTime(row.Cells["StartDate"].Value);
                DateTime endDate = Convert.ToDateTime(row.Cells["EndDate"].Value);

                // Kiểm tra nếu ngày được chọn nằm trong khoảng [startDate, endDate]
                if (ngayChon >= startDate && ngayChon <= endDate)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void dateT_ValueChanged(object sender, EventArgs e)
        {
            LocGiaTheoNgay(dtpFilter.Value.Date);
        }

        private void dgvPrices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
