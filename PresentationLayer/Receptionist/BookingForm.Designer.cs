namespace PresentationLayer.Receptionist
{
    partial class BookingForm
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
            this.cbbGuest = new System.Windows.Forms.ComboBox();
            this.lblRooms = new System.Windows.Forms.Label();
            this.txtCheckinTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCheckoutTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGuestBooking
            // 
            this.lblGuestBooking.AutoSize = true;
            this.lblGuestBooking.Location = new System.Drawing.Point(67, 50);
            this.lblGuestBooking.Name = "lblGuestBooking";
            this.lblGuestBooking.Size = new System.Drawing.Size(42, 16);
            this.lblGuestBooking.TabIndex = 0;
            this.lblGuestBooking.Text = "Guest";
            // 
            // cbbGuest
            // 
            this.cbbGuest.FormattingEnabled = true;
            this.cbbGuest.Location = new System.Drawing.Point(133, 42);
            this.cbbGuest.Name = "cbbGuest";
            this.cbbGuest.Size = new System.Drawing.Size(173, 24);
            this.cbbGuest.TabIndex = 1;
            this.cbbGuest.SelectedIndexChanged += new System.EventHandler(this.cbbGuest_SelectedIndexChanged);
            // 
            // lblRooms
            // 
            this.lblRooms.AutoSize = true;
            this.lblRooms.Location = new System.Drawing.Point(20, 94);
            this.lblRooms.Name = "lblRooms";
            this.lblRooms.Size = new System.Drawing.Size(89, 16);
            this.lblRooms.TabIndex = 2;
            this.lblRooms.Text = "Checkin Time";
            // 
            // txtCheckinTime
            // 
            this.txtCheckinTime.Location = new System.Drawing.Point(133, 88);
            this.txtCheckinTime.Name = "txtCheckinTime";
            this.txtCheckinTime.Size = new System.Drawing.Size(173, 22);
            this.txtCheckinTime.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Checkout Time";
            // 
            // txtCheckoutTime
            // 
            this.txtCheckoutTime.Location = new System.Drawing.Point(133, 130);
            this.txtCheckoutTime.Name = "txtCheckoutTime";
            this.txtCheckoutTime.Size = new System.Drawing.Size(173, 22);
            this.txtCheckoutTime.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Room Booking";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(133, 176);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(173, 22);
            this.txtRoom.TabIndex = 7;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(231, 223);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 279);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtRoom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCheckoutTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCheckinTime);
            this.Controls.Add(this.lblRooms);
            this.Controls.Add(this.cbbGuest);
            this.Controls.Add(this.lblGuestBooking);
            this.Name = "BookingForm";
            this.Text = "BookingForm";
            this.Load += new System.EventHandler(this.BookingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGuestBooking;
        private System.Windows.Forms.ComboBox cbbGuest;
        private System.Windows.Forms.Label lblRooms;
        private System.Windows.Forms.TextBox txtCheckinTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCheckoutTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.Button btnConfirm;
    }
}