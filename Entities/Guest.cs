using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Guest
    {
        public int GuestID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GuestPrivateInfo { get; set; }

        public Guest(string fullname, string phoneNumber, string email, string guestPrivateInfo)
        {
            FullName = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            GuestPrivateInfo = guestPrivateInfo;
        }
        public Guest(int guestid, string fullname, string phoneNumber, string email, string guestPrivateInfo)
        {
            GuestID = guestid;
            FullName = fullname;
            PhoneNumber = phoneNumber;
            Email = email;
            GuestPrivateInfo = guestPrivateInfo;
        }

        public override bool Equals(object obj)
        {
            return obj is Guest guest &&
                   GuestID == guest.GuestID;
        }

        public override int GetHashCode()
        {
            return -2085779058 + GuestID.GetHashCode();
        }
    }
}
