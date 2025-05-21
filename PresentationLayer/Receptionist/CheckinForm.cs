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
        private List<Guest> listNameBooking = new List<Guest>();
        private List<Guest> listNameGuest = new List<Guest>();
        private string bookingID;
        private List<string> bookingNames = new List<string>();
        private List<string> guestNames = new List<string>();

        public CheckinForm()
        {
            InitializeComponent();
        }

        private async void CheckinForm_Load(object sender, EventArgs e)
        {
            listNameBooking = await DataAccessLayer.ListNameSearch.GetListName("Booking");
            listNameGuest = await DataAccessLayer.ListNameSearch.GetListName("Guest");

            guestNames = listNameGuest.Select(g => g.FullName).ToList();
            bookingNames = listNameBooking.Select(g => g.FullName).ToList();
        }

        private void txtGuestBooking_TextChanged(object sender, EventArgs e)
        {
            lstGuestBooking.Visible = true;
            string keyword = txtGuestBooking.Text.ToLower();
            var filteredGuests = bookingNames
                .Where(name => name.ToLower().Contains(keyword))
                .ToList();

            LoadGuestBookings(filteredGuests);

        }
        private void LoadGuestBookings(List<string> guests)
        {
            lstGuestBooking.Items.Clear();
            foreach (var guest in guests)
            {
                lstGuestBooking.Items.Add(guest);
            }
        }

        private async void lstGuestBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txtGuestBooking.Text = lstGuestBooking.SelectedItem.ToString();

            bookingID = await DataAccessLayer.GetBookingIDbyNameDAL.GetBookingIDbyFullname(txtGuestBooking.Text);

            var roomIDs = await DataAccessLayer.GetRoomIDbyBookingID.GetRoomIDbyBooking(bookingID);

            if (roomIDs != null)
            {
                txtRooms.Text = string.Join(", ", roomIDs);
            }
            else
            {
                MessageBox.Show("❌ Không tìm thấy phòng nào cho Booking này.");
            }
            lstGuestBooking.Visible = false;


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


            DataAccessLayer.CheckinDAL.Checkin(stayPeriod, guestIDs);
            MessageBox.Show("✅ Check-in thành công!");

        }
    }
}
