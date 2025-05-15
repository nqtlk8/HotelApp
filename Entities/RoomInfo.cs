using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoomInfo
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public RoomInfo(int roomID, string roomType, int capacity,string description)
        {
            RoomID = roomID;
            RoomType = roomType;
            Capacity = capacity;
            Description = description;
        }
        public override bool Equals(object obj)
        {
            return obj is RoomInfo info &&
                   RoomID == info.RoomID;
        }

        public override int GetHashCode()
        {
            return 819052591 + RoomID.GetHashCode();
        }
    }
}
