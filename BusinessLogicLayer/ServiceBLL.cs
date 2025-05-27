using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer
{
    public static class ServiceBLL
    {
        // Lấy danh sách tất cả dịch vụ
        public static async Task<List<ServiceInfo>> GetAllServices()
        {
            return await ServiceDAL.GetAllServices();
        }

        // Thêm dịch vụ mới
        public static async Task<bool> AddService(string name, string description, int isActive)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var service = new ServiceInfo(name, description, isActive);
            return await ServiceDAL.AddService(service);
        }

        // Cập nhật dịch vụ
        public static async Task<bool> UpdateService(ServiceInfo service)
        {

            return await ServiceDAL.UpdateService(service);
        }

        // Vô hiệu hóa dịch vụ
        public static async Task<bool> DisableService(int id)
        {
            if (id <= 0) return false;

            return await ServiceDAL.DisableService(id);
        }

        public static async Task<List<ServicePrice>> GetServicePricesByServiceID(int serviceID)
        {
            return await ServicePriceDAL.GetPricesByServiceIDAsync(serviceID);
        }
        public class ServicePriceBLL
        {
            public static async Task<bool> UpdateServicePriceAsync(ServicePrice servicePrice)
            {
                return await ServicePriceDAL.UpdateServicePriceAsync(servicePrice);
            }
        }


    }
}
