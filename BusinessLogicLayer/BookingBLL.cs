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


    }
}
