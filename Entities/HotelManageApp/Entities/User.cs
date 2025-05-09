using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Thêm thuộc tính Role
        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
        public User()
        {
        }
    }
}
