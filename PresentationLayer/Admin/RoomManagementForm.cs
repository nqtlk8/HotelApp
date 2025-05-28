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

namespace PresentationLayer.Admin
{
    public partial class RoomManagementForm: Form
    {
        public RoomManagementForm()
        {
            InitializeComponent();
        }
        private int _selectedRoomID = -1;

        private async void RoomManagementForm_Load(object sender, EventArgs e)
        {
            await LoadRoomsAsync();
        }

        private async Task LoadRoomsAsync()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new string[] { "Sẵn sàng", "Bảo trì" });

            flowLayoutPanelRooms.Controls.Clear();

            List<RoomInfo> rooms = await RoomBLL.GetAllRoomsAsync();

            foreach (RoomInfo room in rooms)
            {
                Panel panel = new Panel
                {
                    Width = 125,
                    Height = 120,
                    Margin = new Padding(5),
                    BackColor = GetStatusColor(room.Status),
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Tag = room
                };

                Label lblId = new Label
                {
                    Text = $"Phòng {room.RoomID}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 25,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblType = new Label
                {
                    Text = $"Loại: {room.RoomType}",
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblCap = new Label
                {
                    Text = $"Sức chứa: {room.Capacity}",
                    Dock = DockStyle.Top,
                    Height = 20,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblStatus = new Label
                {
                    Text = $"{room.Status}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                panel.Controls.Add(lblStatus);
                panel.Controls.Add(lblCap);
                panel.Controls.Add(lblType);
                panel.Controls.Add(lblId);

                panel.Click += Panel_Click;
                lblId.Click += (s, ev) => Panel_Click(panel, ev);
                lblType.Click += (s, ev) => Panel_Click(panel, ev);
                lblCap.Click += (s, ev) => Panel_Click(panel, ev);
                lblStatus.Click += (s, ev) => Panel_Click(panel, ev);

                flowLayoutPanelRooms.Controls.Add(panel);
            }
        }

        private async void Panel_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null && sender is Label lbl && lbl.Parent is Panel parentPanel)
                panel = parentPanel;

            var room = panel?.Tag as RoomInfo;
            if (room != null)
            {
                _selectedRoomID = room.RoomID; // Gán giá trị RoomID được chọn

                txtRoomID.Text = room.RoomID.ToString();
                txtRoomType.Text = room.RoomType;
                txtCapacity.Text = room.Capacity.ToString();
                txtDescription.Text = room.Description;
                cboStatus.SelectedItem = room.Status;
            }
            await LoadRoomPricesToDGV(room.RoomID);
        }



        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "Sẵn sàng": return Color.Black;
                case "đang sử dụng": return Color.Orange;
                case "bảo trì": return Color.LightGray;
                default: return Color.WhiteSmoke;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập mã phòng
            if (!int.TryParse(txtRoomID.Text, out int roomId) || roomId <= 0)
            {
                MessageBox.Show("⚠️ Mã phòng phải là số nguyên dương.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRoomType.Text) ||
                string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                string.IsNullOrWhiteSpace(cboStatus.Text))
            {
                MessageBox.Show("⚠️ Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("⚠️ Sức chứa phải là số nguyên dương.");
                return;
            }

            RoomInfo room = new RoomInfo
            {
                RoomID = roomId,
                RoomType = txtRoomType.Text.Trim(),
                Capacity = capacity,
                Description = txtDescription.Text.Trim(),
                Status = cboStatus.SelectedItem.ToString()
            };

            bool result = await RoomBLL.AddRoom(room);
            if (result)
            {
                MessageBox.Show("✅ Thêm phòng thành công!");
                await LoadRoomsAsync();  // Load lại danh sách phòng
                ClearInputFields();      // Xóa dữ liệu trong các ô nhập liệu (nếu muốn)
            }
            else
            {
                MessageBox.Show("❌ Không thể thêm phòng.");
            }
        }
        private void ClearInputFields()
        {
            txtRoomID.Clear();
            txtRoomType.Clear();
            txtCapacity.Clear();
            txtDescription.Clear();
            cboStatus.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();


        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedRoomID <= 0)
            {
                MessageBox.Show("⚠️ Vui lòng chọn phòng cần cập nhật.");
                return;
            }

            if (!int.TryParse(txtRoomID.Text, out int newRoomID) || newRoomID <= 0)
            {
                MessageBox.Show("⚠️ RoomID không hợp lệ.");
                return;
            }

            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("⚠️ Sức chứa phải là số nguyên dương.");
                return;
            }

           

            RoomInfo room = new RoomInfo
            {
                RoomID = newRoomID,
                RoomType = txtRoomType.Text.Trim(),
                Capacity = capacity,
                Description = txtDescription.Text.Trim(),
                Status = cboStatus.SelectedItem?.ToString() ?? ""
            };

            bool updated = await RoomBLL.UpdateRoomIncludingID(_selectedRoomID, room);
            if (updated)
            {
                MessageBox.Show("✅ Cập nhật phòng thành công!");
                await LoadRoomsAsync();
                ClearInputFields();
                _selectedRoomID = -1;
            }
            else
            {
                MessageBox.Show("❌ Không thể cập nhật phòng.");
            }
        }
        private async Task LoadRoomPricesToDGV(int roomId)
        {
            var prices = await RoomBLL.GetAllPricesByRoomId(roomId);

            dgvRoomPrices.DataSource = null;
            dgvRoomPrices.AutoGenerateColumns = false;
            dgvRoomPrices.Columns.Clear();

            void AddColumn(string header, string propName, string format = null)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    HeaderText = header,
                    DataPropertyName = propName,
                    DefaultCellStyle = new DataGridViewCellStyle()
                };
                if (!string.IsNullOrEmpty(format))
                    col.DefaultCellStyle.Format = format;
                dgvRoomPrices.Columns.Add(col);
            }

            AddColumn("Từ ngày", "StartDate", "dd/MM/yyyy");
            AddColumn("Đến ngày", "EndDate", "dd/MM/yyyy");
            AddColumn("Giá (VNĐ)", "Price", "N0");

            dgvRoomPrices.DataSource = prices;
            dgvRoomPrices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private async void btnAddPrice_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập Price
            if (!double.TryParse(txtPrice.Text, out double price) || price <= 0)
            {
                MessageBox.Show("⚠️ Giá phải là số dương hợp lệ.");
                return;
            }

            // Lấy ngày bắt đầu và kết thúc từ DateTimePicker
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("⚠️ Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");
                return;
            }

            // Giả sử _selectedRoomID đã được chọn (ví dụ từ panel click)
            if (_selectedRoomID <= 0)
            {
                MessageBox.Show("⚠️ Vui lòng chọn phòng trước khi thêm giá.");
                return;
            }

            RoomPrice priceInfo = new RoomPrice
            {
                RoomID = _selectedRoomID,
                Price = price,
                StartDate = startDate,
                EndDate = endDate
            };

            bool added = await RoomBLL.AddRoomPrice(priceInfo);
            if (added)
            {
                MessageBox.Show("✅ Thêm giá phòng thành công!");
                await LoadRoomPricesToDGV(_selectedRoomID); // load lại bảng giá nếu có
            }
            else
            {
                MessageBox.Show("❌ Không thể thêm giá phòng.");
            }
        }

        // Event handler dùng index cột
        private void dgvRoomPrices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoomPrices.SelectedRows.Count == 0)
            {
                ClearPriceInputs();
                return;

            }

            var row = dgvRoomPrices.SelectedRows[0];

            // Lấy theo index cột: 0 = StartDate, 1 = EndDate, 2 = Price
            txtPrice.Text = row.Cells[2].Value?.ToString() ?? "";

            if (DateTime.TryParse(row.Cells[0].Value?.ToString(), out DateTime startDate))
                dtpStartDate.Value = startDate;

            if (DateTime.TryParse(row.Cells[1].Value?.ToString(), out DateTime endDate))
                dtpEndDate.Value = endDate;
        }
        private void ClearPriceInputs()
        {
            txtPrice.Clear();
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
        }

        private void btnClearP_Click(object sender, EventArgs e)
        {
            ClearPriceInputs();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
