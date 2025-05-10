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

namespace DataAccessLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            try
            {
                GuestDAL guestDAL = new GuestDAL();
                guestDAL.InitializeConnection();
                List<Guest> guests = guestDAL.GetAllGuests();
                dataGridView1.DataSource = guests;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách: " + ex.Message + "\n" + ex.StackTrace);
            }
        }




    }
}
