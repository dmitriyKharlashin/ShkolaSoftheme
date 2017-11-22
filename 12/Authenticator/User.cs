using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class User : IUser, IDataInfo
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        
        public string GetFullInfo()
        {
            return "User data: name- {Name}, email- {Email}";
        }

        public void DisplayFullInfo()
        {
            Console.WriteLine(GetFullInfo());
        }
    }
}
