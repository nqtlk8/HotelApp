using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Linq;

namespace BusinessLogicLayer
{
    public static class ChartBLL
    {
        // Add this method for occupancy rate
        public static async Task<DataTable> GetOccupancyRateByDateRange(DateTime startDate, DateTime endDate, string timeGrouping)
        {
            // You need to implement this in your DAL as well
            return await DataAccessLayer.ChartDAL.GetOccupancyRateByDateRange(startDate, endDate, timeGrouping);
        }

        // Add this method for loyal customers
        public static async Task<DataTable> GetLoyalCustomers(DateTime startDate, DateTime endDate, int topCount)
        {
            // You need to implement this in your DAL as well
            return await DataAccessLayer.ChartDAL.GetLoyalCustomers(startDate, endDate, topCount);
        }

        // Add this method for service performance
        public static async Task<DataTable> GetServicePerformance(DateTime startDate, DateTime endDate)
        {
            // You need to implement this in your DAL as well
            return await DataAccessLayer.ChartDAL.GetServicePerformance(startDate, endDate);
        }

        // Overload for revenue by date range with time grouping
        public static async Task<DataTable> GetRevenueByDateRange(DateTime startDate, DateTime endDate, string timeGrouping)
        {
            // You need to implement this in your DAL as well
            return await DataAccessLayer.ChartDAL.GetRevenueByDateRange(startDate, endDate, timeGrouping);
        }
    }
}