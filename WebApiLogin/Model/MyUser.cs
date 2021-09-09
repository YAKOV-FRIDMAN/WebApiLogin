using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLogin.Model
{
    public class MyUser
    {
        public MyUser()
        {
                
        }
        public MyUser(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; set; }
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MyUser user &&
                   User == user.User &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(User, Password);
        }

    }
}
