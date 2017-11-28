using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    interface IUserDataBase : IDisposable
    {
        IUser this[int index] { get;set; }
        IUser FindByEmail(string email);
        IUser FindByName(string name);
        void Add(IUser user);
        IUser[] GetUsers();
        void DisplayFullInfo();
    }
}
