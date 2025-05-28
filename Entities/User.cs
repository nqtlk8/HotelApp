using System;

namespace Entities
{
    public class User
    {
        // Private fields
        private string _username;
        private string _password;
        private string _role;

        // Constructor
        public User(string username, string password, string role)
        {
            _username = username;
            _password = password;
            _role = role;
        }

        // Public properties
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
    }
}
