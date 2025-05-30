﻿using System;
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
    public partial class MainForm: Form
    {
        public MainForm()
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

        private void btnRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new RoomManagementForm());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ServiceManagementForm());
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            DashboardForm adminForm = new DashboardForm();
            adminForm.Show();
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GuestCreateForm());
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
