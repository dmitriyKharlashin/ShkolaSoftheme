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
            Console.WriteLine("To qiut from app, plase, enter \"exit\"");
            using (UserDataBase userDataBase = UserDataBase.GetInstance())
            {
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
                        if (name == "exit")
                        {
                            break;
                        }
                        Console.WriteLine("Enter your password:");
                        var password = Console.ReadLine().Trim(' ');
                        if (String.IsNullOrEmpty(password))
                        {
                            throw new NullReferenceException();
                        }
                        if (password == "exit")
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

            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
