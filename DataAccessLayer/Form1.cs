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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DataAccessLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadUserAsync();
        }
        private async void LoadUserAsync()
        {
            User user = await AuthDAL.GetUser("admin");

            if (user != null)
            {
                MessageBox.Show($"Username: {user.Username}, FullName: {user.Password}");
            }
            else
            {
                MessageBox.Show("User not found");
            }
        }
    }
}
