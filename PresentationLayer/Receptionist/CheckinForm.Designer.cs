namespace PresentationLayer.Receptionist
{
    partial class CheckinForm
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
            this.lblGuestBooking = new System.Windows.Forms.Label();
            this.txtGuestBooking = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRooms = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGuests = new System.Windows.Forms.TextBox();
            this.lstGuests = new System.Windows.Forms.ListBox();
            this.btnCheckin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGuestBooking
            // 
            this.lblGuestBooking.AutoSize = true;
            this.lblGuestBooking.Location = new System.Drawing.Point(50, 85);
            this.lblGuestBooking.Name = "lblGuestBooking";
            this.lblGuestBooking.Size = new System.Drawing.Size(95, 16);
            this.lblGuestBooking.TabIndex = 0;
            this.lblGuestBooking.Text = "Guest Booking";
            // 
            // txtGuestBooking
            // 
            this.txtGuestBooking.Location = new System.Drawing.Point(152, 85);
            this.txtGuestBooking.Name = "txtGuestBooking";
            this.txtGuestBooking.Size = new System.Drawing.Size(202, 22);
            this.txtGuestBooking.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rooms";
            // 
            // txtRooms
            // 
            this.txtRooms.Location = new System.Drawing.Point(152, 132);
            this.txtRooms.Name = "txtRooms";
            this.txtRooms.Size = new System.Drawing.Size(202, 22);
            this.txtRooms.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Guests";
            // 
            // txtGuests
            // 
            this.txtGuests.Location = new System.Drawing.Point(152, 170);
            this.txtGuests.Multiline = true;
            this.txtGuests.Name = "txtGuests";
            this.txtGuests.Size = new System.Drawing.Size(202, 71);
            this.txtGuests.TabIndex = 5;
            this.txtGuests.TextChanged += new System.EventHandler(this.txtGuests_TextChanged);
            // 
            // lstGuests
            // 
            this.lstGuests.FormattingEnabled = true;
            this.lstGuests.ItemHeight = 16;
            this.lstGuests.Location = new System.Drawing.Point(152, 247);
            this.lstGuests.Name = "lstGuests";
            this.lstGuests.Size = new System.Drawing.Size(202, 116);
            this.lstGuests.TabIndex = 6;
            this.lstGuests.Visible = false;
            this.lstGuests.SelectedIndexChanged += new System.EventHandler(this.lstGuests_SelectedIndexChanged);
            // 
            // btnCheckin
            // 
            this.btnCheckin.Location = new System.Drawing.Point(226, 387);
            this.btnCheckin.Name = "btnCheckin";
            this.btnCheckin.Size = new System.Drawing.Size(128, 34);
            this.btnCheckin.TabIndex = 8;
            this.btnCheckin.Text = "Check-in";
            this.btnCheckin.UseVisualStyleBackColor = true;
            this.btnCheckin.Click += new System.EventHandler(this.btnCheckin_Click);
            // 
            // CheckinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 450);
            this.Controls.Add(this.btnCheckin);
            this.Controls.Add(this.lstGuests);
            this.Controls.Add(this.txtGuests);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRooms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGuestBooking);
            this.Controls.Add(this.lblGuestBooking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CheckinForm";
            this.Text = "CheckinForm";
            this.Load += new System.EventHandler(this.CheckinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGuestBooking;
        private System.Windows.Forms.TextBox txtGuestBooking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRooms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGuests;
        private System.Windows.Forms.ListBox lstGuests;
        private System.Windows.Forms.Button btnCheckin;
    }
}