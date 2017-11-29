using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class EmailAuthenticator : IAuthenticator
    {
        public IUser AuthenticateUser(string email, string pass)
        {
            AbstractValidator validator = new EmailValidator();
            validator.Validate(email);
            UserDataBase database = UserDataBase.GetInstance();
            IUser user = null;
            IUser foundedUser = database.FindByEmail(email);
            if (foundedUser == null)
            {
                user = new User("", email, pass);
                database.Add(user);
            } else if(foundedUser.Password == pass)
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
