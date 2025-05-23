using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using Entities;

namespace PresentationLayer.Receptionist
{
    public partial class CheckinForm : Form
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private List<Guest> listNameGuest = new List<Guest>();
        public string bookingID { get; set; }
        public string GuestBooking { get; set; }
        private List<string> guestNames = new List<string>();

        public CheckinForm()
        {
            InitializeComponent();
        }

        private async void CheckinForm_Load(object sender, EventArgs e)
        {
            listNameGuest = await DataAccessLayer.ListNameSearch.GetListName("Guest");

            guestNames = listNameGuest.Select(g => g.FullName).ToList();
            txtGuestBooking.Text = GuestBooking;
            List<string> rooms = await DataAccessLayer.GetRoomIDbyBookingID.GetRoomIDbyBooking(bookingID);
            txtRooms.Text = string.Join(", ", rooms);
        }

        
     

        private void txtGuests_TextChanged(object sender, EventArgs e)
        {
            string text = txtGuests.Text;
            string[] parts = text.Split(',');

            // Lấy phần cuối cùng để search
            string keyword = parts.Last().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                lstGuests.Visible = false;
                lstGuests.Items.Clear();
                return;
            }

            var filteredGuests = guestNames
                .Where(name => name.ToLower().Contains(keyword))
                .ToList();

            LoadGuests(filteredGuests);
            lstGuests.Visible = filteredGuests.Any();
        }
        private void LoadGuests(List<string> guests)
        {
            lstGuests.Items.Clear();
            foreach (var guest in guests)
            {
                lstGuests.Items.Add(guest);
            }
        }

        private void lstGuests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGuests.SelectedItem == null) return;

            string selectedName = lstGuests.SelectedItem.ToString();
            string text = txtGuests.Text;
            string[] parts = text.Split(',');

            // Thay phần cuối cùng bằng tên vừa chọn
            parts[parts.Length - 1] = " " + selectedName;

            // Ghép lại chuỗi
            txtGuests.Text = string.Join(",", parts).Trim();

            // Đặt lại con trỏ về cuối
            txtGuests.SelectionStart = txtGuests.Text.Length;
            txtGuests.SelectionLength = 0;

            // Ẩn ListBox
            lstGuests.Visible = false;
        }

        private void btnCheckin_Click(object sender, EventArgs e)
        {
            StayPeriod stayPeriod = new StayPeriod(Convert.ToInt32(bookingID), DateTime.Now);
            List<int> guestIDs = new List<int>();
            string[] guestNames = txtGuests.Text.Split(',');
            foreach (var guestName in guestNames)
            {
                var guestID = listNameGuest
                    .FirstOrDefault(g => g.FullName.Trim().Equals(guestName.Trim(), StringComparison.OrdinalIgnoreCase))
                    ?.GuestID;
                if (guestID != null)
                {
                    guestIDs.Add(Convert.ToInt32(guestID));
                }
            }
            logger.Info($"Đã thực hiện check-in cho BookingID: {bookingID} với khách: {string.Join(", ", guestNames)}");

            DataAccessLayer.CheckinDAL.Checkin(stayPeriod, guestIDs);
            
            MessageBox.Show("✅ Check-in thành công!");
            

        }
    }
}
