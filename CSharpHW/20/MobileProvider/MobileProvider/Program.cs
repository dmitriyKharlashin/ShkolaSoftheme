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
            ValidationContext context1 = new ValidationContext(mobileAccount1);
            if (mobileAccount1.Validate(context1).ToList().Count == 0)
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
            ValidationContext context2 = new ValidationContext(mobileAccount2);
            if (mobileAccount2.Validate(context2).ToList().Count == 0)
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
            ValidationContext context3 = new ValidationContext(mobileAccount3);
            if (mobileAccount3.Validate(context3).ToList().Count == 0)
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
            ValidationContext context4 = new ValidationContext(mobileAccount4);
            if (mobileAccount4.Validate(context4).ToList().Count == 0)
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
            ValidationContext context5 = new ValidationContext(mobileAccount5);
            if (mobileAccount5.Validate(context5).ToList().Count == 0)
            {
                mobileProviderVodaphone.AddAccount(mobileAccount5);
                Console.WriteLine(mobileAccount5);
            }
            Console.WriteLine();

        }
    }
}
