namespace PresentationLayer.Receptionist
{
    partial class BookingServiceForm
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
            this.dgvBooking = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtSelectedServicesInfo = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelServices = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtBookingInfo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooking)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.dgvBooking);
            this.panel1.Location = new System.Drawing.Point(700, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 382);
            this.panel1.TabIndex = 0;
            // 
            // dgvBooking
            // 
            this.dgvBooking.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvBooking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooking.Location = new System.Drawing.Point(29, 34);
            this.dgvBooking.Name = "dgvBooking";
            this.dgvBooking.RowHeadersWidth = 51;
            this.dgvBooking.RowTemplate.Height = 24;
            this.dgvBooking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooking.Size = new System.Drawing.Size(634, 314);
            this.dgvBooking.TabIndex = 0;
            this.dgvBooking.SelectionChanged += new System.EventHandler(this.dgvBooking_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Controls.Add(this.txtBookingInfo);
            this.panel2.Location = new System.Drawing.Point(700, 503);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 236);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnConfirm);
            this.panel3.Controls.Add(this.txtSelectedServicesInfo);
            this.panel3.Controls.Add(this.flowLayoutPanelServices);
            this.panel3.Location = new System.Drawing.Point(45, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(634, 651);
            this.panel3.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.SteelBlue;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfirm.Location = new System.Drawing.Point(331, 376);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(117, 40);
            this.btnConfirm.TabIndex = 34;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtSelectedServicesInfo
            // 
            this.txtSelectedServicesInfo.Location = new System.Drawing.Point(23, 441);
            this.txtSelectedServicesInfo.Multiline = true;
            this.txtSelectedServicesInfo.Name = "txtSelectedServicesInfo";
            this.txtSelectedServicesInfo.Size = new System.Drawing.Size(582, 172);
            this.txtSelectedServicesInfo.TabIndex = 1;
            // 
            // flowLayoutPanelServices
            // 
            this.flowLayoutPanelServices.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanelServices.Location = new System.Drawing.Point(23, 34);
            this.flowLayoutPanelServices.Name = "flowLayoutPanelServices";
            this.flowLayoutPanelServices.Size = new System.Drawing.Size(582, 314);
            this.flowLayoutPanelServices.TabIndex = 0;
            this.flowLayoutPanelServices.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelServices_Paint);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(206, 376);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 40);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtBookingInfo
            // 
            this.txtBookingInfo.Location = new System.Drawing.Point(29, 21);
            this.txtBookingInfo.Multiline = true;
            this.txtBookingInfo.Name = "txtBookingInfo";
            this.txtBookingInfo.Size = new System.Drawing.Size(634, 191);
            this.txtBookingInfo.TabIndex = 36;
            // 
            // BookingServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1439, 855);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookingServiceForm";
            this.Text = "BookingService";
            this.Load += new System.EventHandler(this.BookingService_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooking)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvBooking;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelServices;
        private System.Windows.Forms.TextBox txtSelectedServicesInfo;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtBookingInfo;
    }
}