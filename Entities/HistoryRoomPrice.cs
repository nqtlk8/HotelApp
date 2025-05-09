using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HistoryRoomPrice
    {
        public string RoomID { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
