using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using BusinessLogicLayer;

namespace PresentationLayer.Receptionist
{
    public partial class BookingForm : Form
    {
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public List<int> RoomIDs { get; set; }
        public int GuestID { get; set; }
        public string FullName { get; set; }
        public BookingForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblRooms_Click(object sender, EventArgs e)
        {

        }

        private async void BookingForm_Load(object sender, EventArgs e)
        {
            List<Entities.Guest> guests = await DataAccessLayer.GetGuestDAL.GetGuests();
            if (guests != null)
            {
                foreach (var guest in guests)
                {
                    this.GuestID = guest.GuestID;
                    this.FullName = guest.FullName;
                    cbbGuest.Items.Add(guest.FullName);
                }
            }
            else
            {
                MessageBox.Show("❌ Không có khách nào trong danh sách.");
            }
        }

        private void cbbGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCheckinTime.Text = CheckinTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtCheckoutTime.Text = CheckoutTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtRoom.Text = string.Join(", ", RoomIDs);
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking(GuestID, FullName, CheckinTime, CheckoutTime, 10);
            int addbooking = await DataAccessLayer.AddBookingDAL.AddBooking(booking, RoomIDs);
            if (addbooking > 0)
            {
                MessageBox.Show("✅ Đặt phòng thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Đặt phòng thất bại!");
            }
        }
    }
}
