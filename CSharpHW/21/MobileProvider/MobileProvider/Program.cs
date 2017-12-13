using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using ProtoBuf;

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


            MobileAccount mobileAccount1 = new MobileAccount
            (
                name: "HH.Jnr",
                surname: "Smith",
                birthYear: 1220,
                email: "account@mail.com"
            );

            if (mobileAccount1.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount1);
            }
            Console.WriteLine(mobileAccount1);
            Console.WriteLine();

            MobileAccount mobileAccount2 = new MobileAccount
            (
                name: "",
                surname: "Smith",
                birthYear: 1970,
                email: "hhh"
            );

            if (mobileAccount2.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount2);
            }
            Console.WriteLine(mobileAccount2);
            Console.WriteLine();

            MobileAccount mobileAccount3 = new MobileAccount
            (
                name: "Alian",
                surname: "Smith",
                birthYear: 1980,
                email: "test_1980@account.co.uk"
            );

            if (mobileAccount3.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount3);
            }
            Console.WriteLine(mobileAccount3);
            Console.WriteLine();

            MobileAccount mobileAccount4 = new MobileAccount
            (
                name: "Obrey",
                surname: "Johnson",
                birthYear: 1955,
                email: "acccs@account.co.uk"
            );

            if (mobileAccount4.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount4);
            }
            Console.WriteLine(mobileAccount4);
            Console.WriteLine();

            MobileAccount mobileAccount5 = new MobileAccount
            (
                name: "Salmon",
                surname: "Spencer",
                birthYear: 1934,
                email: "acccs@dkkdk.co.uk"
            );

            if (mobileAccount5.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount5);
            }
            Console.WriteLine(mobileAccount5);
            Console.WriteLine();

            AdminAccount adminAccount = new AdminAccount
            (
                name: "Adrian",
                surname: "Smith",
                birthYear: 1984,
                email: "ssasccdd@dkkdk.co.uk"
            );
            if (adminAccount.Validate())
            {
                Console.WriteLine("Admin is valid");
                mobileProviderVodaphone.AddAccount(adminAccount);
            }
            Console.WriteLine(adminAccount);
            Console.WriteLine();

            MobileAccount mobileAccount6 = new MobileAccount
            (
                name: "Zack",
                surname: "Manninger",
                birthYear: 1934,
                email: "mannn@dkkdk.com"
            );
            if (mobileAccount6.Validate())
            {
                mobileProviderVodaphone.AddAccount(mobileAccount6);
            }
            Console.WriteLine(mobileAccount6);
            Console.WriteLine();

            mobileAccount3?.AddAddress(new Dictionary<int, string>()
            {
                {0951000004, "Allain"},
                {0951000003, "Edgar"},
                {0951000002, "Milan"},
            });
            mobileAccount4?.AddAddress(0951000001, "Albert");
            mobileAccount5?.AddAddress(new Dictionary<int, string>()
            {
                {0951000002, "Marge"},
                {0951000001, "Vins"},
                {0951000003, "Robert"},
            });

            List<MobileAccountDO> accounts = mobileProviderVodaphone.ConvertIntoDO().Accounts;
            Stopwatch timeSpanBinary = new Stopwatch();
            timeSpanBinary.Restart();
            BinaryParser binaryParser = new BinaryParser();
            binaryParser.ParseIntoFile("accounts.dat", accounts);
            timeSpanBinary.Stop();
            Console.WriteLine($"Binary serialization - {timeSpanBinary.ElapsedMilliseconds}");

            Stopwatch timeSpanJson = new Stopwatch();
            timeSpanJson.Restart();
            JsonParser<List<MobileAccountDO>> jsonParser = new JsonParser<List<MobileAccountDO>>();
            jsonParser.ParseIntoFile("accounts.json", accounts);
            timeSpanJson.Stop();
            Console.WriteLine($"JSON serialization - {timeSpanJson.ElapsedMilliseconds}");
            
            Stopwatch timeSpanXml = new Stopwatch();
            timeSpanXml.Restart();
            XmlParser<List<MobileAccountDO>> xmlParser = new XmlParser<List<MobileAccountDO>>();
            xmlParser.ParseIntoFile("accounts.xml", accounts);
            timeSpanXml.Stop();
            Console.WriteLine($"XML serialization - {timeSpanXml.ElapsedMilliseconds}");
            
            //Stopwatch timeSpanSoap = new Stopwatch();
            //timeSpanSoap.Restart();
            //SoapParser soapParser = new SoapParser();
            //soapParser.ParseIntoFile<List<MobileAccountDO>>("accounts.xml", accounts);
            //timeSpanSoap.Stop();
            //Console.WriteLine($"Soap serialization - {timeSpanSoap.ElapsedMilliseconds}");

            Stopwatch timeSpanProto = new Stopwatch();
            timeSpanProto.Restart();
            ProtoBufParser protoParser = new ProtoBufParser();
            protoParser.ParseIntoFile("accounts.bin", accounts);
            timeSpanProto.Stop();
            Console.WriteLine($"ProtoBuf serialization - {timeSpanProto.ElapsedMilliseconds}");
        }

    }

    [Serializable]
    public class Person
    {
        public string Name;

        public int Age;

        public Person()
        {

        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
