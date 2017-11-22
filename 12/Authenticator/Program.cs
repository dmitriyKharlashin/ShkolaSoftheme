using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("User authenticator app!");
            Console.WriteLine("To qiut from app, plase, enter \":q\"");
            do
            {
                try
                {
                    Console.WriteLine("Enter your login:");
                    var name = Console.ReadLine().Trim(' ');
                    if (String.IsNullOrEmpty(name))
                    {
                        throw new NullReferenceException();
                    }
                    if (name == ":q")
                    {
                        break;
                    }
                    Console.WriteLine("Enter your password:");
                    var password = Console.ReadLine().Trim(' ');
                    if (String.IsNullOrEmpty(password))
                    {
                        throw new NullReferenceException();
                    }
                    if (password == ":q")
                    {
                        break;
                    }
                    string authenticationType = name.Contains('@') ? "email" : "name";
                    IAuthenticator authenticator = (new AuthenticationSelector()).GetAutenticator(authenticationType);
                    IUser user = authenticator.AuthenticateUser(name, password);
                    user.DisplayFullInfo();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            while (true);

            Database database = Database.GetInstance();
            database.DisplayFullInfo();
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
