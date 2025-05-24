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
        public static async Task<bool> AddService(string name, string description, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var service = new ServiceInfo(name, description, isActive ? 1 : 0);
            return await ServiceDAL.AddService(service);
        }

        // Cập nhật dịch vụ
        public static async Task<bool> UpdateService(int id, string name, string description, bool isActive)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name)) return false;

            var service = new ServiceInfo(id, name, description, isActive ? 1 : 0);
            return await ServiceDAL.UpdateService(service);
        }

        // Vô hiệu hóa dịch vụ
        public static async Task<bool> DisableService(int id)
        {
            if (id <= 0) return false;

            return await ServiceDAL.DisableService(id);
        }
    }
}
