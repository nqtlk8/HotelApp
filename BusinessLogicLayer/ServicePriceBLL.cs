using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogicLayer
{
    public static class ServicePriceBLL
    {
        public static async Task<bool> AddServicePriceAsync(ServicePrice newPrice)
        {
            // Kiểm tra logic dữ liệu trước khi gọi DAL
            if (newPrice.StartDate >= newPrice.EndDate)
            {
                MessageBox.Show("❌ Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
                return false;
            }

            return await ServicePriceDAL.InsertServicePriceAsync(
                newPrice.ServiceID,
                newPrice.ServicePriceValue,
                newPrice.StartDate,
                newPrice.EndDate
            );

        }
        public static async Task<bool> UpdateServicePriceAsync(ServicePrice servicePrice)
        {
            // Kiểm tra logic dữ liệu trước khi gọi DAL
            if (servicePrice.StartDate >= servicePrice.EndDate)
            {
                MessageBox.Show("❌ Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
                return false;
            }
            return await ServicePriceDAL.UpdateServicePriceAsync(servicePrice);
        }
    }

}
