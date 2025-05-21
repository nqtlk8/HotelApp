namespace PresentationLayer.Receptionist
{
    partial class ReceptionistMainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBooking = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flpRoomList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblRoomTypeSelected = new System.Windows.Forms.Label();
            this.clbRoomType = new System.Windows.Forms.CheckedListBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnCheckin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCheckin);
            this.panel1.Controls.Add(this.btnBooking);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 450);
            this.panel1.TabIndex = 1;
            // 
            // btnBooking
            // 
            this.btnBooking.Location = new System.Drawing.Point(37, 266);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(113, 69);
            this.btnBooking.TabIndex = 0;
            this.btnBooking.Text = "Booking";
            this.btnBooking.UseVisualStyleBackColor = true;
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flpRoomList);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 450);
            this.panel2.TabIndex = 2;
            // 
            // flpRoomList
            // 
            this.flpRoomList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRoomList.Location = new System.Drawing.Point(0, 117);
            this.flpRoomList.Name = "flpRoomList";
            this.flpRoomList.Size = new System.Drawing.Size(600, 333);
            this.flpRoomList.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblRoomTypeSelected);
            this.panel3.Controls.Add(this.clbRoomType);
            this.panel3.Controls.Add(this.lblTo);
            this.panel3.Controls.Add(this.lblFrom);
            this.panel3.Controls.Add(this.dateTimePicker2);
            this.panel3.Controls.Add(this.dateTimePicker1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 117);
            this.panel3.TabIndex = 0;
            // 
            // lblRoomTypeSelected
            // 
            this.lblRoomTypeSelected.AutoSize = true;
            this.lblRoomTypeSelected.Location = new System.Drawing.Point(336, 27);
            this.lblRoomTypeSelected.Name = "lblRoomTypeSelected";
            this.lblRoomTypeSelected.Size = new System.Drawing.Size(79, 16);
            this.lblRoomTypeSelected.TabIndex = 5;
            this.lblRoomTypeSelected.Text = "RoomType:";
            // 
            // clbRoomType
            // 
            this.clbRoomType.FormattingEnabled = true;
            this.clbRoomType.Location = new System.Drawing.Point(418, 21);
            this.clbRoomType.Name = "clbRoomType";
            this.clbRoomType.Size = new System.Drawing.Size(120, 72);
            this.clbRoomType.TabIndex = 4;
            this.clbRoomType.SelectedIndexChanged += new System.EventHandler(this.clbRoomType_SelectedIndexChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(35, 55);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 16);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(21, 27);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(41, 16);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(97, 49);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(97, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnCheckin
            // 
            this.btnCheckin.Location = new System.Drawing.Point(37, 352);
            this.btnCheckin.Name = "btnCheckin";
            this.btnCheckin.Size = new System.Drawing.Size(113, 69);
            this.btnCheckin.TabIndex = 1;
            this.btnCheckin.Text = "Check-In";
            this.btnCheckin.UseVisualStyleBackColor = true;
            this.btnCheckin.Click += new System.EventHandler(this.btnCheckin_Click);
            // 
            // ReceptionistMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ReceptionistMainForm";
            this.Text = "ReceptionistMainForm";
            this.Load += new System.EventHandler(this.ReceptionistMainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flpRoomList;
        private System.Windows.Forms.CheckedListBox clbRoomType;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblRoomTypeSelected;
        private System.Windows.Forms.Button btnBooking;
        private System.Windows.Forms.Button btnCheckin;
    }
}