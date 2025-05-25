namespace PresentationLayer.Receptionist
{
    partial class CheckinList
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
            this.lstCheckin = new System.Windows.Forms.ListView();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstCheckin
            // 
            this.lstCheckin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCheckin.HideSelection = false;
            this.lstCheckin.Location = new System.Drawing.Point(0, 0);
            this.lstCheckin.Name = "lstCheckin";
            this.lstCheckin.Size = new System.Drawing.Size(800, 450);
            this.lstCheckin.TabIndex = 0;
            this.lstCheckin.UseCompatibleStateImageBehavior = false;
            this.lstCheckin.View = System.Windows.Forms.View.Details;
            this.lstCheckin.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstCheckin_DrawColumnHeader);
            this.lstCheckin.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstCheckin_DrawItem);
            this.lstCheckin.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstCheckin_DrawSubItem_1);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(629, 365);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(113, 50);
            this.btnCheckout.TabIndex = 1;
            this.btnCheckout.Text = "Check-out";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // CheckinList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.lstCheckin);
            this.Name = "CheckinList";
            this.Text = "CheckinList";
            this.Load += new System.EventHandler(this.CheckinList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstCheckin;
        private System.Windows.Forms.Button btnCheckout;
    }
}