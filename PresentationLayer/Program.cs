using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresentationLayer.Admin;
using PresentationLayer.Receptionist;
namespace PresentationLayer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            LoginForm loginForm = new LoginForm();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu loginForm trả về OK thì chạy MainForm
                Application.Run(new ReceptionistMainForm());
            }
            */
            MainForm mainForm = new MainForm();
            Application.Run(new MainForm());
        }
    }
}
