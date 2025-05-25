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
using NLog;
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

            bool isAuthenticated = await AuthBLL.Login(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Đăng nhập thành công");
                CurrentUser.Username = username; // Lưu thông tin người dùng hiện tại

                this.DialogResult = DialogResult.OK;
                // Gán username vào MDC để log ghi nhận
                NLog.GlobalDiagnosticsContext.Set("Username", username);

                logger.Info("Đăng nhập thành công");

                NLog.LogManager.Flush(); // 🟢 đảm bảo ghi log xong trước khi đóng
                this.Close(); // đóng form đăng nhập
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng");
            }
        }
    }
}
