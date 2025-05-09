using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer
{
    public class AuthBLL
    {
        public static bool Login(string username, string password)
        {
            var user = AuthDAL.GetUser(username);
            if (user == null) return false;

            // So sánh mật khẩu (ở đây là plain text, thực tế nên hash)
            return user.Password == password;
        }
    }
}
