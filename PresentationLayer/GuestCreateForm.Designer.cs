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
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(210, 158);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(250, 22);
            this.txtFullName.TabIndex = 6;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(210, 198);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(250, 22);
            this.txtPhoneNumber.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(210, 238);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 22);
            this.txtEmail.TabIndex = 8;
            // 
            // txtIDCardNumber
            // 
            this.txtIDCardNumber.Location = new System.Drawing.Point(727, 156);
            this.txtIDCardNumber.Name = "txtIDCardNumber";
            this.txtIDCardNumber.Size = new System.Drawing.Size(250, 22);
            this.txtIDCardNumber.TabIndex = 9;
            // 
            // txtPassportNumber
            // 
            this.txtPassportNumber.Location = new System.Drawing.Point(727, 196);
            this.txtPassportNumber.Name = "txtPassportNumber";
            this.txtPassportNumber.Size = new System.Drawing.Size(250, 22);
            this.txtPassportNumber.TabIndex = 10;
            // 
            // txtCCCDNumber
            // 
            this.txtCCCDNumber.Location = new System.Drawing.Point(727, 236);
            this.txtCCCDNumber.Name = "txtCCCDNumber";
            this.txtCCCDNumber.Size = new System.Drawing.Size(250, 22);
            this.txtCCCDNumber.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(791, 293);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(888, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvGuestList
            // 
            this.dgvGuestList.AllowUserToAddRows = false;
            this.dgvGuestList.AllowUserToDeleteRows = false;
            this.dgvGuestList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGuestList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvGuestList.ColumnHeadersHeight = 29;
            this.dgvGuestList.Location = new System.Drawing.Point(73, 348);
            this.dgvGuestList.Name = "dgvGuestList";
            this.dgvGuestList.ReadOnly = true;
            this.dgvGuestList.RowHeadersWidth = 51;
            this.dgvGuestList.Size = new System.Drawing.Size(924, 303);
            this.dgvGuestList.TabIndex = 14;
            // 
            // lblFullName
            // 
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblFullName.ForeColor = System.Drawing.Color.Navy;
            this.lblFullName.Location = new System.Drawing.Point(83, 157);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(100, 23);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name:";
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.Navy;
            this.lblPhoneNumber.Location = new System.Drawing.Point(83, 197);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(100, 23);
            this.lblPhoneNumber.TabIndex = 1;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblEmail.ForeColor = System.Drawing.Color.Navy;
            this.lblEmail.Location = new System.Drawing.Point(83, 237);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 23);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // lblIDCard
            // 
            this.lblIDCard.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblIDCard.ForeColor = System.Drawing.Color.Navy;
            this.lblIDCard.Location = new System.Drawing.Point(587, 156);
            this.lblIDCard.Name = "lblIDCard";
            this.lblIDCard.Size = new System.Drawing.Size(100, 23);
            this.lblIDCard.TabIndex = 3;
            this.lblIDCard.Text = "CMND:";
            // 
            // lblPassport
            // 
            this.lblPassport.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblPassport.ForeColor = System.Drawing.Color.Navy;
            this.lblPassport.Location = new System.Drawing.Point(587, 196);
            this.lblPassport.Name = "lblPassport";
            this.lblPassport.Size = new System.Drawing.Size(100, 23);
            this.lblPassport.TabIndex = 4;
            this.lblPassport.Text = "Passport:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F);
            this.lblCCCD.ForeColor = System.Drawing.Color.Navy;
            this.lblCCCD.Location = new System.Drawing.Point(587, 236);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(100, 23);
            this.lblCCCD.TabIndex = 5;
            this.lblCCCD.Text = "CCCD:";
            // 
            // GuestCreateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1103, 740);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "GuestCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guest Information";
            this.Load += new System.EventHandler(this.GuestCreateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuestList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
