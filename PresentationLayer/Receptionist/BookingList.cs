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
using DataAccessLayer;

namespace PresentationLayer.Receptionist
{
    public partial class BookingList : Form
    {
        private List<Booking> bookings;
        public BookingList()
        {
            InitializeComponent();
        }

        private async void BookingList_Load(object sender, EventArgs e)
        {
            lstBookings.View = View.Details;
            lstBookings.FullRowSelect = true;
            lstBookings.GridLines = true;
            lstBookings.HideSelection = false;
            lstBookings.MultiSelect = false;
            lstBookings.OwnerDraw = true;
            lstBookings.Columns.Clear();
            lstBookings.Items.Clear();

            // Khai báo các cột
            lstBookings.Columns.Add("Booking ID", 100);
            lstBookings.Columns.Add("Guest ID", 100);
            lstBookings.Columns.Add("Full Name", 150);
            lstBookings.Columns.Add("Check-in", 120);
            lstBookings.Columns.Add("Check-out", 120);
            lstBookings.Columns.Add("Total Price", 100);
            bookings = await DataAccessLayer.GetBookingDAL.GetBooking();
            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    ListViewItem item = new ListViewItem(booking.BookingID.ToString());
                    item.SubItems.Add(booking.GuestID.ToString());
                    item.SubItems.Add(booking.FullName);
                    item.SubItems.Add(booking.CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(booking.CheckOutDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(booking.TotalPrice.ToString());
                    lstBookings.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("❌ Không tìm thấy người dùng nào.");
            }
        }

        private void lstBookings_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var backBrush = new SolidBrush(Color.LightSkyBlue))
            using (var textBrush = new SolidBrush(Color.Black))
            using (var font = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.Header.Text, font, textBrush, e.Bounds);
            }
        }

        private void lstBookings_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Không cần vẽ item ở đây, tránh đè nếu có subitem
            e.DrawDefault = false;
        }

        private void lstBookings_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color bgColor = e.ItemIndex % 2 == 0 ? Color.White : Color.LightGray;
            Color textColor = Color.Black;

            if (e.Item.Selected)
            {
                bgColor = Color.DodgerBlue;
                textColor = Color.White;
            }

            using (var backgroundBrush = new SolidBrush(bgColor))
            using (var textBrush = new SolidBrush(textColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
                e.Graphics.DrawString(e.SubItem.Text, e.Item.Font, textBrush, e.Bounds);
            }
        }

        private void lstBookings_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckinForm checkinForm = new CheckinForm();
            checkinForm.GuestBooking = lstBookings.SelectedItems[0].SubItems[2].Text;
            checkinForm.bookingID = lstBookings.SelectedItems[0].SubItems[0].Text;

            checkinForm.ShowDialog();

        }
    }
}
