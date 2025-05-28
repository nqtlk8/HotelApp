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
    public static class RoomBLL
    {
        public static async Task<List<string>> GetRoomType()
        {
            try
            {
                return await GetRoomTypeDAL.GetRoomType();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message);
                return null;
            }
        }
        public static async Task<List<RoomInfo>> GetRoomInfo()
        {
            try
            {
                return await GetRoomInfoDAL.GetRoomInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error: " + ex.Message);
                return null;
            }
        }
        public static async Task<List<RoomInfo>> GetAllRoomsAsync()
        {
            return await RoomDAL.GetAllRoomsAsync();
        }

        public static async Task<bool> AddRoom(RoomInfo room)
        {
            if (room.RoomID > 0)
            {
                bool exists = await RoomDAL.IsRoomIDExists(room.RoomID);
                if (exists)
                {
                    MessageBox.Show("⚠️ Mã phòng đã tồn tại, vui lòng chọn số khác.");
                    return false;
                }
            }
            // Gọi DAL để thêm
            return await RoomDAL.AddRoom(room);

        }
        public static async Task<bool> UpdateRoomIncludingID(int oldRoomID, RoomInfo room)
        {
            return await RoomDAL.UpdateRoomIncludingID(oldRoomID, room);
        }

        public static async Task<List<RoomPrice>> GetAllPricesByRoomId(int roomId)
        {
            return await RoomPriceDAL.GetAllPricesByRoomId(roomId);
        }
        public static async Task<bool> AddRoomPrice(RoomPrice priceInfo)
        {
            if (priceInfo.Price <= 0) return false;
            if (priceInfo.EndDate < priceInfo.StartDate) return false;

            bool isOverlap = await RoomPriceDAL.IsPricePeriodOverlapping(priceInfo.RoomID, priceInfo.StartDate, priceInfo.EndDate);
            if (isOverlap)
            {
                MessageBox.Show("⚠️ Khoảng thời gian giá phòng bị trùng với giá hiện tại. Vui lòng chọn khoảng thời gian khác.");
                return false;
            }

            return await RoomPriceDAL.AddRoomPrice(priceInfo);
        }

        public static async Task<bool> UpdateRoomPrice(int roomId, DateTime newStart, DateTime newEnd, double newPrice, int currentPriceId)
        {
            // Lấy danh sách bản ghi trùng khoảng thời gian, loại trừ chính bản ghi hiện tại
            var overlappingPrices = await RoomPriceDAL.GetOverlappingPrices(roomId, newStart, newEnd, excludePriceId: currentPriceId);

            if (overlappingPrices.Any())
            {
                // Có bản ghi khác trùng khoảng thời gian => không cho phép cập nhật
                return false;
            }

            // Không trùng với bản ghi khác => cập nhật
            return await RoomPriceDAL.UpdateRoomPriceDetails(currentPriceId, roomId, newStart, newEnd, newPrice);
        }

    }
}
   
    