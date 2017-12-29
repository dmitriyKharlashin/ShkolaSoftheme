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
        private static Provider _mobileProviderVodaphone;
        private static MobileAccount _mobileAccount;
        private static IParser _parser;

        static void Main(string[] args)
        {
            // add new mobile provider
            _mobileProviderVodaphone = new Provider("Vodaphone");
            // initialize loggers
            EventLogger eventLogger = new EventLogger();
            CallLogger callLogger = new CallLogger();

            // subscribe loggers into provider events
            _mobileProviderVodaphone.DeliveringSmsAction += eventLogger.AddMessageEvent;
            _mobileProviderVodaphone.DeliveringCallAction += eventLogger.AddCallEvent;
            _mobileProviderVodaphone.DeliveringSmsAction += callLogger.AddMessageEvent;
            _mobileProviderVodaphone.DeliveringCallAction += callLogger.AddCallEvent;

            _parser = new JsonConvertParser<MobileAccountDO>();

            var filePath = "accounts.json";
            GetConvertedDataFromFile(filePath);

            Console.WriteLine($"Welcome, to the Mobile Book App!");
            InitNewAccountCommand();

            InitNextStepCommand();

            SetConvertedDataIntoFile(filePath);
        }

        static void InitAccount()
        {
            Console.WriteLine($"Please, enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine($"Please, enter your surname:");
            string surname = Console.ReadLine();
            Console.WriteLine($"Please, enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine($"Please, enter your birth year(format 4 digits):");
            int birthYear = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            _mobileAccount = new MobileAccount(name, surname, email, birthYear);
        }

        static void InitAddress(ref int number, ref string name)
        {
            Console.WriteLine("Please, enter new contact`s name:");
            name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Please, re-enter correct name");
            }

            Console.WriteLine("Please, enter new contact`s phone number:");
            number = Int32.Parse(Console.ReadLine());
            if (number == 0)
            {
                throw new Exception("Please, re-enter correct phone number");
            }
        }

        private static void GetConvertedDataFromFile(string filePath)
        {
            _parser.ParseFromFile(filePath, out List<MobileAccountDO> data);

            if (data != null && data.Count > 0)
            {
                foreach (MobileAccountDO mobileAccount in data)
                {
                    _mobileProviderVodaphone.AddAccount(mobileAccount.FromDataObject());
                }
            }
        }

        private static void GetDataFromFile(string filePath)
        {
            _parser.ParseFromFile(filePath, out List<MobileAccount> data);

            if (data != null && data.Count > 0)
            {
                foreach (MobileAccount mobileAccount in data)
                {
                    _mobileProviderVodaphone.AddAccount(mobileAccount);
                }
            }
        }

        public static void SetConvertedDataIntoFile(string filePath)
        {
            _parser.ParseIntoFile(filePath, _mobileProviderVodaphone.ToDataObject().Accounts);
        }

        public static void SetDataIntoFile(string filePath)
        {
            _parser.ParseIntoFile(filePath, _mobileProviderVodaphone.Accounts);
        }

        private static void InitNewAccountCommand()
        {
            while (true)
            {
                try
                {
                    InitAccount();
                    if (_mobileAccount.Validate())
                    {
                        Console.WriteLine(_mobileAccount);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void InitNewAddressCommand()
        {
            try
            {
                int number = 0;
                string name = null;
                InitAddress(ref number, ref name);
                _mobileAccount.AddAddress(number, name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                InitNewAddressCommand();
            }
        }

        private static void InitNextStepCommand()
        {
            Console.WriteLine($"To add a new contact into address book enter: \"-nc\"");
            Console.WriteLine($"To change the mobile account enter: \"-chac\"");
            Console.WriteLine($"To exit from the APP enter: \"-q\"");

            var command = Console.ReadLine();
            switch (command)
            {
                case "-chac":
                    _mobileProviderVodaphone.AddAccount(_mobileAccount);
                    InitNewAccountCommand();
                    break;
                case "-nc":
                    InitNewAddressCommand();
                    break;
                case "-q":
                    _mobileProviderVodaphone.AddAccount(_mobileAccount);
                    return;
            }

            InitNextStepCommand();
        }
    }
}
