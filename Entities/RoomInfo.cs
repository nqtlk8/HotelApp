using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoomInfo
    {
        public string RoomID { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public RoomInfo(string roomID, string roomType, int capacity)
        {
            RoomID = roomID;
            RoomType = roomType;
            Capacity = capacity;
        }
        public override bool Equals(object obj)
        {
            return obj is RoomInfo info &&
                   RoomID == info.RoomID;
        }
        public override int GetHashCode()
        {
            return -2085779058 + EqualityComparer<string>.Default.GetHashCode(RoomID);
        }
    }
}
