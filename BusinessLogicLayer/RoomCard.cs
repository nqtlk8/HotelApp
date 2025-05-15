using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogicLayer
{
    public partial class RoomCard : UserControl
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        private bool _isAvailable;

        public bool isAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                UpdateAvailabilityDisplay();
            }
        }
        public bool isSelected { get; set; } = false;
        public RoomCard(int roomID, string roomType, int capacity, string description)
        {
            InitializeComponent();
            this.RoomID = roomID;
            this.RoomType = roomType;
            this.Capacity = capacity;
            this.Description = description;

            this.Click += RoomCard_Click;
            foreach (Control control in this.Controls)
            {
                control.Click += RoomCard_Click;
            }

            AttachMouseEvents(this); 
        }
        public RoomCard()
        {
            InitializeComponent();

            this.Click += RoomCard_Click;
            foreach (Control control in this.Controls)
            {
                control.Click += RoomCard_Click; // Đảm bảo click vào label, panel con cũng trigger
            }
            // Gán sự kiện MouseEnter và MouseLeave cho toàn bộ control trong RoomCard
            // Để tạo hiệu ứng hover
            AttachMouseEvents(this);
        }

        private void RoomCard_Load(object sender, EventArgs e)
        {
            lblCapacity.Text = "Capacity: "+Capacity.ToString();
            lblRoomType.Text = "Type: "+RoomType;
            lblRoomID.Text = RoomID.ToString();
            lblDescription.Text = Description;
            if (isAvailable)
            {
                lblIsAvaiable.Text = "Available";
                lblIsAvaiable.ForeColor = System.Drawing.Color.Lime;
            }
            else
            {
                lblIsAvaiable.Text = "Booked";
                lblIsAvaiable.ForeColor = System.Drawing.Color.Red;
            }
        }
        public event EventHandler<int> RoomCardClicked; // int là RoomID
        private void RoomCard_Click(object sender, EventArgs e)
        {
            // Cập nhật trạng thái chọn
            isSelected = !isSelected;
            this.BackColor = isSelected ? Color.LightGreen : Color.White;  // Thay đổi màu nền khi chọn

            RoomCardClicked?.Invoke(this, this.RoomID);  // Trigger event
        }
        private void AttachMouseEvents(Control parent)
        {
            parent.MouseEnter += (s, e) =>
            {
                if (!isSelected)
                    this.BackColor = Color.LightGray;
            };

            parent.MouseLeave += (s, e) =>
            {
                if (!isSelected)
                    this.BackColor = Color.White;
            };

            foreach (Control child in parent.Controls)
            {
                AttachMouseEvents(child);
            }
        }
        private void UpdateAvailabilityDisplay()
        {
            if (lblIsAvaiable == null) return; // Phòng khi label chưa được khởi tạo

            if (_isAvailable)
            {
                lblIsAvaiable.Text = "Available";
                lblIsAvaiable.ForeColor = Color.Lime;
            }
            else
            {
                lblIsAvaiable.Text = "Booked";
                lblIsAvaiable.ForeColor = Color.Red;
            }
        }
    }
}
