using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using DataAccessLayer;
using Entities;
using NLog;
using PresentationLayer.Admin;
using PresentationLayer.Receptionist;
using Shared;

namespace PresentationLayer
{
    public partial class LoginForm : Form
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu");
                return;
            }

            // Lấy thông tin user từ database
            User user = await AuthDAL.GetUser(username);

            if (user != null && user.Password == password)
            {
                if (string.IsNullOrEmpty(user.Role))
                {
                    MessageBox.Show("⚠ Người dùng chưa được phân quyền, vui lòng liên hệ quản trị viên.");
                    return;
                }

                NLog.GlobalDiagnosticsContext.Set("Username", username);
                logger.Info("Đăng nhập thành công");

                Form mainForm = null;
                switch (user.Role)
                {
                    case "admin":
                        mainForm = new MainForm();
                        break;
                    case "Receptionist":
                        mainForm = new MainFormRecep();
                        break;
                    default:
                        MessageBox.Show("⚠ Vai trò không hợp lệ hoặc chưa được hỗ trợ.");
                        return;
                }

                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
            }
        }
    }
    }
