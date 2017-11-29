using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class AuthenticationSelector
    {
        public IAuthenticator GetAutenticator(string type)
        {
            IAuthenticator authenticator = null;
            switch(type)
            {
                case "name":
                    authenticator = new NameAuthenticator();
                    break;
                case "email":
                    authenticator = new EmailAuthenticator();
                    break;
            }

            return authenticator;
        }
    }
}
