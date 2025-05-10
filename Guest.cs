using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    
        public class Guest
        {
            public string GuestID { get; set; }
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string GuestPrivateInf { get; set; }

            // Constructor đầy đủ
            public Guest(string fullName, string phoneNumber, string email, string guestPrivateInf)
            {
                FullName = fullName;
                PhoneNumber = phoneNumber;
                Email = email;
                GuestPrivateInf = guestPrivateInf;
            }

            // Constructor rỗng (CẦN cho DataGridView binding)
            public Guest() { }
        }
    }


