using BusinessLogicLayer;
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

namespace PresentationLayer.Admin
{
    public partial class ServiceManagementForm: Form
    {
        public ServiceManagementForm()
        {
            InitializeComponent();
            this.TopLevel = false;       // Cho phép form này được nhúng
            this.FormBorderStyle = FormBorderStyle.None;  // Ẩn viền
            this.Dock = DockStyle.Fill;  // Co giãn toàn Panel
            LoadData();
        }
        private async void LoadData()
        {
            var data = await ServiceBLL.GetAllServices();
            dgvServices.DataSource = data;

            // Ẩn cột ServiceID
            dgvServices.Columns["ServiceID"].Visible = false;

            // Cấu hình tiêu đề các cột
            dgvServices.Columns["ServiceName"].HeaderText ="Dịch vụ";
            dgvServices.Columns["Descrip"].HeaderText = "Mô tả";
            dgvServices.Columns["IsActive"].HeaderText = "Trạng thái";


            // Không dùng Fill để có thể cuộn
            dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServices.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvServices.ScrollBars = ScrollBars.Both;
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
                txtIsActive.Text = row.Cells["IsActive"].Value?.ToString() ?? "";
                LoadPriceData();
            }
            
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            string name = txtServiceName.Text.Trim();
            string descrip = txtDescrip.Text.Trim();
            int isActive;

            // Kiểm tra hợp lệ đầu vào
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("⚠️ Vui lòng nhập tên dịch vụ.");
                return;
            }

            if (!int.TryParse(txtIsActive.Text.Trim(), out isActive) || (isActive != 0 && isActive != 1))
            {
                MessageBox.Show("⚠️ Trạng thái hoạt động chỉ được là 0 hoặc 1.");
                return;
            }

            
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
            txtIsActive.Clear();

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

            int isActive;

            if (!int.TryParse(txtIsActive.Text.Trim(), out isActive) || (isActive != 0 && isActive != 1))
            {
                MessageBox.Show("⚠️ Trạng thái hoạt động chỉ được là 0 hoặc 1.");
                return;
            }

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
    }
}
