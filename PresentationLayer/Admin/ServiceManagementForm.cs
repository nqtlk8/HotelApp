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
            dgvServices.Columns["ServiceName"].HeaderText = "Tên dịch vụ";
            dgvServices.Columns["Descrip"].HeaderText = "Mô tả";
            dgvServices.Columns["IsActive"].HeaderText = "Trạng thái";


            // Không dùng Fill để có thể cuộn
            dgvServices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvServices.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvServices.ScrollBars = ScrollBars.Both;
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

        private void btnAdd_Click(object sender, EventArgs e)
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
                await LoadServiceListAsync();

                // Xóa input
                btnClear.PerformClick();
            }
        }

        private void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow != null)
            {
                var row = dgvServices.CurrentRow;

                txtServiceName.Text = row.Cells["ServiceName"].Value?.ToString() ?? "";
                txtDescrip.Text = row.Cells["Descrip"].Value?.ToString() ?? "";
                txtIsActive.Text = row.Cells["IsActive"].Value?.ToString() ?? "";
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
    }
}
