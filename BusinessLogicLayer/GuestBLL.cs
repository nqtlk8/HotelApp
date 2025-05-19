using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class GuestBLL
    {

        public static async Task<Guest> GetGuestByInvoiceIDAsync(int invoiceID)
        {
            return await GuestDAL.GetGuestByInvoiceIDAsync(invoiceID);
        }
    }
}
