using PresentationLayer.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Receptionist
{
    public partial class MainFormRecep: Form
    {
        public MainFormRecep()
        {
            InitializeComponent();
        }
        private Form currentChildForm;

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
                currentChildForm.Close();

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.AutoScaleMode = AutoScaleMode.None;

            panelMain.Controls.Clear();
            panelMain.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnBooking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReceptionistMainForm());
        }

        private void btnBookingService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BookingServiceForm());
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GuestCreateForm());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PaymentForm());
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            OpenChildForm(new InvoiceList());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Đóng MainForm hiện tại
            this.Close();

            // Hiển thị lại form đăng nhập
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
