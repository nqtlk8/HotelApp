using System.Windows.Forms;

namespace PresentationLayer
{
    partial class GuestCreateForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtIDCardNumber;
        private System.Windows.Forms.TextBox txtPassportNumber;
        private System.Windows.Forms.TextBox txtCCCDNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvGuestList;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblIDCard;
        private System.Windows.Forms.Label lblPassport;
        private System.Windows.Forms.Label lblCCCD;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtIDCardNumber = new System.Windows.Forms.TextBox();
            this.txtPassportNumber = new System.Windows.Forms.TextBox();
            this.txtCCCDNumber = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvGuestList = new System.Windows.Forms.DataGridView();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblIDCard = new System.Windows.Forms.Label();
            this.lblPassport = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvGuestList)).BeginInit();
            this.SuspendLayout();

            // --- Labels
            this.lblFullName.Text = "Full Name:";
            this.lblFullName.Location = new System.Drawing.Point(20, 20);
            this.lblPhoneNumber.Text = "Phone Number:";
            this.lblPhoneNumber.Location = new System.Drawing.Point(20, 60);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(20, 100);
            this.lblIDCard.Text = "CMND:";
            this.lblIDCard.Location = new System.Drawing.Point(20, 140);
            this.lblPassport.Text = "Passport:";
            this.lblPassport.Location = new System.Drawing.Point(20, 180);
            this.lblCCCD.Text = "CCCD:";
            this.lblCCCD.Location = new System.Drawing.Point(20, 220);

            // --- TextBoxes
            this.txtFullName.Location = new System.Drawing.Point(150, 20);
            this.txtFullName.Size = new System.Drawing.Size(250, 22);

            this.txtPhoneNumber.Location = new System.Drawing.Point(150, 60);
            this.txtPhoneNumber.Size = new System.Drawing.Size(250, 22);

            this.txtEmail.Location = new System.Drawing.Point(150, 100);
            this.txtEmail.Size = new System.Drawing.Size(250, 22);

            this.txtIDCardNumber.Location = new System.Drawing.Point(150, 140);
            this.txtIDCardNumber.Size = new System.Drawing.Size(250, 22);

            this.txtPassportNumber.Location = new System.Drawing.Point(150, 180);
            this.txtPassportNumber.Size = new System.Drawing.Size(250, 22);

            this.txtCCCDNumber.Location = new System.Drawing.Point(150, 220);
            this.txtCCCDNumber.Size = new System.Drawing.Size(250, 22);

            // --- Buttons
            this.btnSave.Text = "Save";
            this.btnSave.Location = new System.Drawing.Point(150, 260);
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new System.Drawing.Point(240, 260);
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // --- DataGridView
            this.dgvGuestList.Location = new System.Drawing.Point(20, 310);
            this.dgvGuestList.Size = new System.Drawing.Size(600, 200);
            this.dgvGuestList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGuestList.ReadOnly = true;
            this.dgvGuestList.AllowUserToAddRows = false;
            this.dgvGuestList.AllowUserToDeleteRows = false;

            // --- Add Controls to Form
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblIDCard);
            this.Controls.Add(this.lblPassport);
            this.Controls.Add(this.lblCCCD);

            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtIDCardNumber);
            this.Controls.Add(this.txtPassportNumber);
            this.Controls.Add(this.txtCCCDNumber);

            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgvGuestList);

            // --- Form Settings
            this.Text = "Guest Information";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(650, 540);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            ((System.ComponentModel.ISupportInitialize)(this.dgvGuestList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
