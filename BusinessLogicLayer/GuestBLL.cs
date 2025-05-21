using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public static class GuestBLL
    {
        public static async Task<List<Guest>> GetGuestsAsync()
        {
            try
            {
                return await GetGuestDAL.GetGuests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error retrieving guests list: " + ex.Message);
                return null;
            }
        }

        public static async Task<Guest> GetGuestByIdAsync(int guestId)
        {
            try
            {
                return await GetGuestDAL.GetGuestById(guestId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error retrieving guest details: " + ex.Message);
                return null;
            }
        }

        public static async Task<int> SaveGuestAsync(Guest guest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(guest.FullName))
                    throw new ArgumentException("Guest name cannot be empty");

                if (string.IsNullOrWhiteSpace(guest.PhoneNumber))
                    throw new ArgumentException("Phone number cannot be empty");

                if (string.IsNullOrWhiteSpace(guest.GuestPrivateInfo))
                    throw new ArgumentException("Guest identification information cannot be empty");

                return await GetGuestDAL.SaveGuest(guest);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error saving guest: " + ex.Message);
                return -1;
            }
        }

        public static async Task<bool> UpdateGuestAsync(Guest guest)
        {
            try
            {
                if (guest.GuestID <= 0)
                    throw new ArgumentException("Invalid guest ID");

                if (string.IsNullOrWhiteSpace(guest.FullName))
                    throw new ArgumentException("Guest name cannot be empty");

                if (string.IsNullOrWhiteSpace(guest.PhoneNumber))
                    throw new ArgumentException("Phone number cannot be empty");

                if (string.IsNullOrWhiteSpace(guest.GuestPrivateInfo))
                    throw new ArgumentException("Guest identification information cannot be empty");

                return await GetGuestDAL.UpdateGuest(guest);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error updating guest: " + ex.Message);
                return false;
            }
        }

        public static async Task<bool> DeleteGuestAsync(int guestId)
        {
            try
            {
                if (guestId <= 0)
                    throw new ArgumentException("Invalid guest ID");

                return await GetGuestDAL.DeleteGuest(guestId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error deleting guest: " + ex.Message);
                return false;
            }
        }
    }
}