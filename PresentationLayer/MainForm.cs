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

            panelMain.Controls.Clear();
            panelMain.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new ServiceManagementForm());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ServiceManagementForm());
        }
    }
}
