using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using Entities;

namespace PresentationLayer.Receptionist
{
    public partial class CheckinList : Form
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private List<Booking> Checkin;
        public CheckinList()
        {
            InitializeComponent();
        }

        private async void CheckinList_Load(object sender, EventArgs e)
        {
            lstCheckin.View = View.Details;
            lstCheckin.FullRowSelect = true;
            lstCheckin.GridLines = true;
            lstCheckin.HideSelection = false;
            lstCheckin.MultiSelect = false;
            lstCheckin.OwnerDraw = true;
            lstCheckin.Columns.Clear();
            lstCheckin.Items.Clear();

            // Khai báo các cột
            lstCheckin.Columns.Add("Booking ID", 100);
            lstCheckin.Columns.Add("Guest ID", 100);
            lstCheckin  .Columns.Add("Full Name", 150);
            lstCheckin.Columns.Add("Check-in", 120);
            lstCheckin.Columns.Add("Check-out", 120);
            lstCheckin.Columns.Add("Total Price", 100);
            Checkin = await DataAccessLayer.GetCheckinDAL.GetCheckin();

            if (Checkin != null)
            {
                foreach (var booking in Checkin)
                {
                    ListViewItem item = new ListViewItem(booking.BookingID.ToString());
                    item.SubItems.Add(booking.GuestID.ToString());
                    item.SubItems.Add(booking.FullName);
                    item.SubItems.Add(booking.CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(booking.CheckOutDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(booking.TotalPrice.ToString());
                    lstCheckin.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("❌ Không tìm thấy người dùng nào.");
            }
        }

       

        private void lstCheckin_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var backBrush = new SolidBrush(Color.LightSkyBlue))
            using (var textBrush = new SolidBrush(Color.Black))
            using (var font = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.Header.Text, font, textBrush, e.Bounds);
            }
        }
        private void lstCheckin_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Không cần vẽ item ở đây, tránh đè nếu có subitem
            e.DrawDefault = false;
        }

        

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if(lstCheckin.SelectedItems.Count == 0)
    {
                MessageBox.Show("❌ Vui lòng chọn một dòng booking để checkout.");
                return;
            }

            // Lấy dòng được chọn
            ListViewItem selectedItem = lstCheckin.SelectedItems[0];

            // Lấy BookingID từ dòng đầu tiên (giả sử BookingID nằm ở cột 0)
            int bookingID = int.Parse(selectedItem.SubItems[0].Text);
            MessageBox.Show( $"{bookingID}");

            // Gọi hàm xử lý Checkout
            DataAccessLayer.CheckoutDAL.CheckoutAsync(bookingID).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    logger.Error(task.Exception, "Lỗi khi thực hiện checkout");
                    MessageBox.Show("❌ Lỗi khi thực hiện checkout.");
                }
                else
                {
                    logger.Info($"Đã thực hiện checkout cho BookingID: {bookingID}");
                    MessageBox.Show("✅ Checkout thành công!");
                    return;
                }
            });

        }

        private void lstCheckin_DrawSubItem_1(object sender, DrawListViewSubItemEventArgs e)
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
    }
}
