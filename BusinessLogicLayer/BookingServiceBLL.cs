using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BookingServiceBLL
    {
        public static async Task<bool> AddBookingServiceAsync(BookingService service)
        {
            return await BookingServiceDAL.InsertBookingServiceAsync(service);
        }

        public static async Task<bool> AddMultipleBookingServicesAsync(List<BookingService> services)
        {
            bool allSuccess = true;

            foreach (var service in services)
            {
                bool result = await BookingServiceDAL.InsertBookingServiceAsync(service);
                if (!result)
                    allSuccess = false;
            }

            return allSuccess;
        }
    }

}
