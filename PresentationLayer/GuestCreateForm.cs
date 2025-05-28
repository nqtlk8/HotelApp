using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using BusinessLogicLayer;

namespace PresentationLayer
{
    public partial class GuestCreateForm : Form
    {
        private bool isEdit = false;
        private int guestId = 0;

        public GuestCreateForm()
        {
            InitializeComponent();
            LoadGuestList();
        }

        public GuestCreateForm(int guestId) : this()
        {
            this.guestId = guestId;
            isEdit = true;
            LoadGuestData();
            this.Text = "Edit Guest Information";
            btnSave.Text = "Update";
        }

        private async void LoadGuestData()
        {
            try
            {
                var guest = await GuestBLL.GetGuestByIdAsync(guestId);
                if (guest != null)
                {
                    txtFullName.Text = guest.FullName;
                    txtPhoneNumber.Text = guest.PhoneNumber;
                    txtEmail.Text = guest.Email;

                    // Parse GuestPrivateInfo
                    string[] lines = guest.GuestPrivateInfo?.Split('\n');
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("CMND"))
                            txtIDCardNumber.Text = line.Split(':')[1].Trim();
                        else if (line.StartsWith("Passport"))
                            txtPassportNumber.Text = line.Split(':')[1].Trim();
                        else if (line.StartsWith("CCCD"))
                            txtCCCDNumber.Text = line.Split(':')[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guest data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadGuestList()
        {
            try
            {
                var guestList = await GuestBLL.GetGuestsAsync();
                dgvGuestList.DataSource = guestList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading guest list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                List<string> privateInfos = new List<string>();

                if (!string.IsNullOrWhiteSpace(txtIDCardNumber.Text))
                    privateInfos.Add($"CMND: {txtIDCardNumber.Text.Trim()}");

                if (!string.IsNullOrWhiteSpace(txtPassportNumber.Text))
                    privateInfos.Add($"Passport: {txtPassportNumber.Text.Trim()}");

                if (!string.IsNullOrWhiteSpace(txtCCCDNumber.Text))
                    privateInfos.Add($"CCCD: {txtCCCDNumber.Text.Trim()}");

                Guest guest = new Guest(
                    isEdit ? guestId : 0,
                    txtFullName.Text.Trim(),
                    txtPhoneNumber.Text.Trim(),
                    txtEmail.Text.Trim(),
                    string.Join("\n", privateInfos)
                );

                if (isEdit)
                    await GuestBLL.UpdateGuestAsync(guest);
                else
                    await GuestBLL.SaveGuestAsync(guest);

                MessageBox.Show("Guest information saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGuestList();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving guest information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtIDCardNumber.Text)
                && string.IsNullOrWhiteSpace(txtPassportNumber.Text)
                && string.IsNullOrWhiteSpace(txtCCCDNumber.Text))
            {
                MessageBox.Show("Please enter at least one of CMND, Passport, or CCCD.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIDCardNumber.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void GuestCreateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
