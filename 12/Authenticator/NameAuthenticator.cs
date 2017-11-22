using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class NameAuthenticator : IAuthenticator
    {
        public IUser AuthenticateUser(string name, string pass)
        {
            AbstractValidator validator = new NameValidator();
            validator.Validate(name);
            Database database = Database.GetInstance();
            IUser user = null;
            IUser foundedUser = database.FindByName(name);
            if (foundedUser == null)
            {
                user = new User("", name, pass);
                database.Add(user);
            }
            else if (foundedUser.Password == pass)
            {
                return foundedUser;
            }

            if (user == null)
            {
                throw new Exception("Password is wrong! Please, try again to login!");
            }
            return user;
        }
    }
}
