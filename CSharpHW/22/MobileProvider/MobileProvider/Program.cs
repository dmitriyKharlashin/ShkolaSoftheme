using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using ProtoBuf;
using System.Collections;

namespace MobileProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            // add new mobile provider
            Provider mobileProviderVodaphone = new Provider("Vodaphone");
            // initialize loggers
            EventLogger eventLogger = new EventLogger();
            CallLogger callLogger = new CallLogger();

            // subscribe loggers into provider events
            mobileProviderVodaphone.DeliveringSmsAction += eventLogger.AddMessageEvent;
            mobileProviderVodaphone.DeliveringCallAction += eventLogger.AddCallEvent;
            mobileProviderVodaphone.DeliveringSmsAction += callLogger.AddMessageEvent;
            mobileProviderVodaphone.DeliveringCallAction += callLogger.AddCallEvent;

            JsonParser<List<MobileAccountDO>> jsonParser = new JsonParser<List<MobileAccountDO>>();
            jsonParser.ParseFromFile("accounts.json", out List<MobileAccountDO> data);

            if (data != null && data.Count > 0)
            {
                foreach (MobileAccountDO mobileAccountDo in data)
                {
                    mobileProviderVodaphone.AddAccount(mobileAccountDo.FromDataObject());
                }
            }

            MobileAccount mobileAccount;

            Console.WriteLine($"Welcome, to the Mobile Book App!");
            while (true)
            {
                try
                {
                    InitAccount(ref mobileProviderVodaphone, out mobileAccount);
                    if (mobileAccount.Validate())
                    {
                        mobileProviderVodaphone.AddAccount(mobileAccount);
                        data?.Add(mobileAccount.ToDataObject());
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

            Console.WriteLine(mobileAccount);
            jsonParser.ParseIntoFile("accounts.json", mobileProviderVodaphone.ToDataObject().Accounts);

            Console.WriteLine($"To add a new contact into address book enter: \"-nc\"");
            Console.WriteLine($"To change the mobile account enter: \"-chac\"");
            Console.WriteLine($"To exit from the APP enter: \"exit\"");


        }

        public static void InitAccount(ref Provider mobileProviderVodaphone, out MobileAccount mobileAccount)
        {
            Console.WriteLine($"Please, enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine($"Please, enter your surname:");
            string surname = Console.ReadLine();
            Console.WriteLine($"Please, enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine($"Please, enter your birth year(format 4 digits):");
            int birthYear = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            mobileAccount = new MobileAccount(name, surname, email, birthYear);
        }

    }
}
