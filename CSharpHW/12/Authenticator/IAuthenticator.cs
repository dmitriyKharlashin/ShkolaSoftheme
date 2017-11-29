using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    interface IAuthenticator
    {
        IUser AuthenticateUser(string login, string pass);
    }
}
