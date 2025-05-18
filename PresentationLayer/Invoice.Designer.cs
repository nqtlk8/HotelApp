namespace PresentationLayer
{
    partial class Invoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.gbGuest = new System.Windows.Forms.GroupBox();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.lblBookingID = new System.Windows.Forms.Label();
            this.gbGuest.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(149, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hóa đơn thanh toán";
            // 
            // gbGuest
            // 
            this.gbGuest.Controls.Add(this.lblCCCD);
            this.gbGuest.Controls.Add(this.lblEmail);
            this.gbGuest.Controls.Add(this.lblSDT);
            this.gbGuest.Controls.Add(this.lblName);
            this.gbGuest.Location = new System.Drawing.Point(26, 138);
            this.gbGuest.Name = "gbGuest";
            this.gbGuest.Size = new System.Drawing.Size(259, 129);
            this.gbGuest.TabIndex = 1;
            this.gbGuest.TabStop = false;
            this.gbGuest.Text = "Thông tin khách hàng";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Location = new System.Drawing.Point(6, 110);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(51, 16);
            this.lblCCCD.TabIndex = 7;
            this.lblCCCD.Text = "label13";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(6, 84);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 16);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "label12";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(6, 57);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(51, 16);
            this.lblSDT.TabIndex = 5;
            this.lblSDT.Text = "label11";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 31);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 16);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "label10";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblCount);
            this.groupBox3.Controls.Add(this.lblCheckOut);
            this.groupBox3.Controls.Add(this.lblCheckIn);
            this.groupBox3.Controls.Add(this.lblBookingID);
            this.groupBox3.Location = new System.Drawing.Point(331, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(252, 129);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông tin đặt phòng";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(6, 110);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(51, 16);
            this.lblCount.TabIndex = 11;
            this.lblCount.Text = "label17";
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(6, 84);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(51, 16);
            this.lblCheckOut.TabIndex = 10;
            this.lblCheckOut.Text = "label16";
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(6, 57);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(51, 16);
            this.lblCheckIn.TabIndex = 9;
            this.lblCheckIn.Text = "label15";
            // 
            // lblBookingID
            // 
            this.lblBookingID.AutoSize = true;
            this.lblBookingID.Location = new System.Drawing.Point(6, 31);
            this.lblBookingID.Name = "lblBookingID";
            this.lblBookingID.Size = new System.Drawing.Size(51, 16);
            this.lblBookingID.TabIndex = 8;
            this.lblBookingID.Text = "label14";
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 746);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbGuest);
            this.Controls.Add(this.label1);
            this.Name = "Invoice";
            this.Text = "Invoice";
            this.gbGuest.ResumeLayout(false);
            this.gbGuest.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbGuest;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.Label lblBookingID;
    }
}