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

        private void Form1_Load(object sender, EventArgs e)
        {
            InsertUser();
        }
        private async void InsertUser()
        {
            string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
            var parameters = new SQLiteParameter[]
            {
    new SQLiteParameter("@Username", "john_doe"),
    new SQLiteParameter("@Password", "123456"),
    new SQLiteParameter("@Role", "Receptionist")
            };

            int result = await UpdateDatabase.Query(insertQuery, parameters);

            if (result > 0)
            {
                MessageBox.Show("✅ Thêm user thành công!");
            }
            else
            {
                MessageBox.Show("❌ Không thể thêm user.");
            }
        }
    }
}
