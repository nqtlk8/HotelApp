using System;
using System.Drawing;
using System.Windows.Forms;

namespace BusinessLogicLayer
{
    partial class RoomCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRoomID = new System.Windows.Forms.Label();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblIsAvaiable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRoomID
            // 
            this.lblRoomID.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRoomID.ForeColor = System.Drawing.Color.Black;
            this.lblRoomID.Location = new System.Drawing.Point(0, 10);
            this.lblRoomID.Name = "lblRoomID";
            this.lblRoomID.Size = new System.Drawing.Size(220, 32);
            this.lblRoomID.TabIndex = 0;
            this.lblRoomID.Text = "101";
            this.lblRoomID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.ForeColor = System.Drawing.Color.BlueViolet;
            this.lblRoomType.Location = new System.Drawing.Point(10, 50);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(110, 16);
            this.lblRoomType.TabIndex = 1;
            this.lblRoomType.Text = "Type: Single Room";
            // 
            // lblCapacity
            // 
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblCapacity.Location = new System.Drawing.Point(10, 70);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(81, 16);
            this.lblCapacity.TabIndex = 2;
            this.lblCapacity.Text = "Capacity: 2";
            // 
            // lblDescription
            // 
            this.lblDescription.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblDescription.Location = new System.Drawing.Point(10, 95);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(200, 50); // wrap text
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Comfortable room with AC, WiFi and a beautiful window view.";
            // 
            // lblIsAvaiable
            // 
            this.lblIsAvaiable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIsAvaiable.ForeColor = System.Drawing.Color.Lime;
            this.lblIsAvaiable.Location = new System.Drawing.Point(0, 150);
            this.lblIsAvaiable.Name = "lblIsAvaiable";
            this.lblIsAvaiable.Size = new System.Drawing.Size(220, 20);
            this.lblIsAvaiable.TabIndex = 4;
            this.lblIsAvaiable.Text = "Available";
            this.lblIsAvaiable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RoomCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; // viền đen mỏng
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblIsAvaiable);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCapacity);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.lblRoomID);
            this.Name = "RoomCard";
            this.Size = new System.Drawing.Size(220, 180);
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Load += new System.EventHandler(this.RoomCard_Load);

            
        }

        #endregion

        private System.Windows.Forms.Label lblRoomID;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblIsAvaiable;
    }
}
