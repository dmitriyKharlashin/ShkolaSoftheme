using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            // add new mobile provider
            IMobileProvider mobileProviderVodaphone = new Provider("Vodaphone");
            // initialize loggers
            EventLogger eventLogger = new EventLogger();
            CallLogger callLogger = new CallLogger();

            // subscribe loggers into provider events
            mobileProviderVodaphone.DeliveringSmsAction += eventLogger.AddMessageEvent;
            mobileProviderVodaphone.DeliveringCallAction += eventLogger.AddCallEvent;
            mobileProviderVodaphone.DeliveringSmsAction += callLogger.AddMessageEvent;
            mobileProviderVodaphone.DeliveringCallAction += callLogger.AddCallEvent;


            MobileAccount mobileAccount1 = new MobileAccount
            (
                name: "HH.Jnr",
                surname: "Smith",
                birthYear: 1220,
                email: "account@mail.com"
            );

            if (Validate(mobileAccount1, mobileProviderVodaphone))
            {
                mobileProviderVodaphone.AddAccount(mobileAccount1);
                Console.WriteLine(mobileAccount1);
            }

            MobileAccount mobileAccount2 = new MobileAccount
            (
                name: "a",
                surname: "Smith",
                birthYear: 1970,
                email: "hhh"
            );
            
            if (Validate(mobileAccount2, mobileProviderVodaphone))
            {
                mobileProviderVodaphone.AddAccount(mobileAccount2);
                Console.WriteLine(mobileAccount2);
            }
            Console.WriteLine();

            MobileAccount mobileAccount3 = new MobileAccount
            (
                name: "Alian",
                surname: "Smith",
                birthYear: 1980,
                email: "test_1980@account.co.uk"
            );

            if (Validate(mobileAccount3, mobileProviderVodaphone))
            {
                mobileProviderVodaphone.AddAccount(mobileAccount3);
                Console.WriteLine(mobileAccount3);
            }
            Console.WriteLine();

            MobileAccount mobileAccount4 = new MobileAccount
            (
                name: "Obrey",
                surname: "Johnson",
                birthYear: 1955,
                email: "acccs@account.co.uk"
            );

            if (Validate(mobileAccount4, mobileProviderVodaphone))
            {
                mobileProviderVodaphone.AddAccount(mobileAccount4);
                Console.WriteLine(mobileAccount4);
            }
            Console.WriteLine();

            MobileAccount mobileAccount5 = new MobileAccount
            (
                name: "Salmon",
                surname: "Spencer",
                birthYear: 1934,
                email: "acccs@dkkdk.co.uk"
            );

            if (Validate(mobileAccount5, mobileProviderVodaphone))
            {
                mobileProviderVodaphone.AddAccount(mobileAccount5);
                Console.WriteLine(mobileAccount5);
            }
            Console.WriteLine();

        }

        static bool Validate(IMobileAccount mobileAccount, IMobileProvider mobileProvider)
        {
            var results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(mobileAccount);
            var isValid = Validator.TryValidateObject(mobileAccount, context, results, true);
            if (!isValid)
            {
                foreach (var error in results)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(error.ErrorMessage);
                    Console.ResetColor();
                }
            }

            return isValid;
        }
    }
}
