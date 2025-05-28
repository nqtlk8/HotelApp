using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class BookingBLL
    {
        public static async Task<Booking> GetBookingByInvoiceIDAsync(int invoiceId)
        {
            return await BookingDAL.GetBookingByInvoiceIDAsync(invoiceId);
        }
        public static async Task<List<Booking>> GetBooking()
        {
            return await BookingDAL.GetBookings();
        }
        public static async Task<List<Booking>> GetBookingsWithStayPeriodAsync()
        {
            return await BookingDAL.GetBookingsWithStayPeriodAsync();
        }

    }
}
