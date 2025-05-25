using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            List<Booking> checkin = await GetCheckinDAL.GetCheckin();
            foreach (var  booking in checkin)
            {
                MessageBox.Show(booking.BookingID.ToString());
            }

        }
        
       
    }
}
