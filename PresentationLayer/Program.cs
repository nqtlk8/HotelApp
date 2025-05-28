using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresentationLayer.Receptionist;
using NLog;
using NLog.Config;
using PresentationLayer.Admin;


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
             MainFormRecep ainFormRecep = new MainFormRecep();
             Application.Run(new MainFormRecep());


             MainForm mainForm = new MainForm();
             Application.Run(new MainForm());

             DashboardForm loginForm = new DashboardForm();

             if (loginForm.ShowDialog() == DialogResult.OK)
             {
                 // Nếu loginForm trả về OK thì chạy MainForm
                 Application.Run(new ReceptionistMainForm());
             }
             */

            //Application.Run(new InvoiceForm(30));

            //Application.Run(new Form());

            //Application.Run(new GuestCreateForm());
            Application.Run(new LoginForm());
        }
    }
}
