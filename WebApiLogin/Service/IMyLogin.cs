using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLogin.Service
{
    public interface IMyLogin
    {
        string Login(string userName, string password);
    }
}
