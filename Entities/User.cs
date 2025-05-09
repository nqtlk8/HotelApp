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
        public string Role { get; set; } 
        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Username == user.Username;
        }

        public override int GetHashCode()
        {
            return -182246463 + EqualityComparer<string>.Default.GetHashCode(Username);
        }
    }
}
