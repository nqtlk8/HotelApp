namespace PresentationLayer.Receptionist
{
    partial class BookingList
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
            this.lstBookings = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstBookings
            // 
            this.lstBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBookings.HideSelection = false;
            this.lstBookings.Location = new System.Drawing.Point(0, 0);
            this.lstBookings.Name = "lstBookings";
            this.lstBookings.Size = new System.Drawing.Size(800, 450);
            this.lstBookings.TabIndex = 0;
            this.lstBookings.UseCompatibleStateImageBehavior = false;
            this.lstBookings.View = System.Windows.Forms.View.Details;
            this.lstBookings.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstBookings_DrawColumnHeader);
            this.lstBookings.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstBookings_DrawItem);
            this.lstBookings.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstBookings_DrawSubItem);
            this.lstBookings.SelectedIndexChanged += new System.EventHandler(this.lstBookings_SelectedIndexChanged);
            // 
            // BookingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstBookings);
            this.Name = "BookingList";
            this.Text = "BookingList";
            this.Load += new System.EventHandler(this.BookingList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstBookings;
    }
}