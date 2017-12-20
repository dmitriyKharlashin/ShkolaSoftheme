using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace MobileProvider
{
    class Program
    {
        static string _folderAddress;

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

            Console.WriteLine($"Welcome, to the Mobile Book App!");

            Console.WriteLine($"Please, enter the directory address to file with accounts (or leave empry to use default folder):");
            while (true)
            {
                try
                {
                    _folderAddress = $@"{Console.ReadLine()}";

                    if (_folderAddress.Equals(String.Empty))
                    {
                        _folderAddress = Directory.GetCurrentDirectory();
                    }
                    else if (!_folderAddress.Equals(string.Empty) && !Directory.Exists(_folderAddress))
                    {
                        Console.WriteLine("Please, re-enter the correct folder address:");
                        continue;
                    }
                    
                    ParseAccountsData(ref mobileProviderVodaphone);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please, re-enter the correct folder address:");
                }
            }

            PopulateAccountsDataFromConsole(ref mobileProviderVodaphone, out var mobileAccount);
            SaveAccountChanges(mobileAccount);

            while (true)
            {
                Console.WriteLine($"To add a new contact into address book enter: \"-nc\"");
                Console.WriteLine($"To change the mobile account enter: \"-chac\"");
                Console.WriteLine($"To exit from the APP enter: \"-q\"");

                bool commandIsCorrect = false;
                while (!commandIsCorrect)
                {
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "-nc":
                            commandIsCorrect = true;

                            int number;
                            string name;
                            PopulateMobileAddressFromConsole(ref mobileAccount, out number, out name);
                            SaveAccountAddresses(mobileAccount, number, name);
                            break;
                        case "-chac":
                            commandIsCorrect = true;
                            PopulateAccountsDataFromConsole(ref mobileProviderVodaphone, out mobileAccount);
                            SaveAccountChanges(mobileAccount);
                            break;
                        case "-q":
                            return;
                        default:
                            commandIsCorrect = false;
                            continue;
                    }
                }
            }
        }

        static void PopulateAccountsDataFromConsole(ref Provider mobileProvider, out MobileAccount mobileAccount)
        {
            while (true)
            {
                try
                {
                    InitAccount(out mobileAccount);
                    if (mobileAccount.Validate())
                    {
                        mobileProvider.AddAccount(mobileAccount);
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void PopulateMobileAddressFromConsole(ref MobileAccount mobileAccount, out int number, out string name)
        {
            while (true)
            {
                try
                {
                    InitAddress(out number, out name);
                    mobileAccount.AddAddress(number, name);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void InitAccount(out MobileAccount mobileAccount)
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

        static void InitAddress(out int number, out string name)
        {
            Console.WriteLine("Please, enter new contact`s name:");
            name = Console.ReadLine();
            if (name.Equals(String.Empty))
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

        static void ParseAccountsData(ref Provider mobileProvider)
        {
            if (File.Exists(_folderAddress + "\\accounts.xml"))
            {
                XDocument xDocument = XDocument.Load(_folderAddress + "\\accounts.xml");
                XElement xRootElement = xDocument.Root;

                var mobileAccounts = xRootElement.Elements("MobileAccount");

                foreach (XElement mobileAccount in mobileAccounts)
                {
                    int number = Int32.Parse(mobileAccount.Element("Number").Value);
                    string name = mobileAccount.Element("Name").Value;
                    string surname = mobileAccount.Element("Surname").Value;
                    string email = mobileAccount.Element("Email").Value;
                    int birthYear = Int32.Parse(mobileAccount.Element("BirthYear").Value);

                    mobileProvider.AddAccount(new MobileAccount(name, surname, email, birthYear)
                    {
                        Number = number
                    });
                }
            }
        }

        static void SaveAccountChanges(MobileAccount mobileAccount)
        {
            XDocument xDocument;
            if (File.Exists(_folderAddress + "\\accounts.xml"))
            {
                xDocument = XDocument.Load(_folderAddress + "\\accounts.xml");
                XElement xRootElement = xDocument.Root;

                XElement[] foundedElements = xRootElement.Elements("MobileAccount")
                    .Where(p => p.Element("Email")?.Value == mobileAccount.Email).Take(1).ToArray();

                if (!foundedElements.Any())
                {
                    xRootElement.Add(PrepareAccountNode(mobileAccount));
                }
                else
                {
                    foundedElements[0].ReplaceWith(PrepareAccountNode(mobileAccount));
                }    
            }
            else
            {
                xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "Yes"),
                    new XElement("MobileAccounts"));
                XElement xRootElement = xDocument.Element("MobileAccounts");
                
                xRootElement?.Add(PrepareAccountNode(mobileAccount));
            }

            xDocument.Save(_folderAddress + "\\accounts.xml");
        }

        static XElement PrepareAccountNode(MobileAccount mobileAccount)
        {

            XElement mobileAccountElement = new XElement("MobileAccount",
                new XElement("Number", mobileAccount.Number),
                new XElement("Name", mobileAccount.Name),
                new XElement("Surname", mobileAccount.Surname),
                new XElement("Email", mobileAccount.Email),
                new XElement("BirthYear", mobileAccount.BirthYear));

            return mobileAccountElement;
        }

        static XElement PrepareAddressNode(int number, string name)
        {
            XElement mobileAddressElement = new XElement("Address",
                new XElement("Name", name),
                new XElement("Number", number)
            );

            return mobileAddressElement;
        }
        
        static void SaveAccountAddresses(MobileAccount mobileAccount, int number, string name)
        {
            XDocument xDocument = XDocument.Load(_folderAddress + "\\accounts.xml");
            XElement xRootElement = xDocument.Root;
            XElement[] foundedAccounts = xRootElement.Elements("MobileAccount")
                .Where(p => p.Element("Email")?.Value == mobileAccount.Email).Take(1).ToArray();

            if (foundedAccounts.Length > 0)
            {
                var mobileAddressesElement = foundedAccounts[0].Element("Addresses");
                if (mobileAddressesElement == null)
                {
                    foundedAccounts[0].Add(new XElement("Addresses"));
                    mobileAddressesElement = foundedAccounts[0].Element("Addresses");
                }

                XElement[] foundedAddresses = mobileAddressesElement.Elements("Address")
                    .Where(p => p.Element("Number").Value.Equals(number)).Take(1).ToArray();

                if (!foundedAddresses.Any())
                {
                    mobileAddressesElement.Add(PrepareAddressNode(number, name));
                }
                else
                {
                    foundedAddresses[0].ReplaceWith(PrepareAddressNode(number, name));
                }
            }

            xDocument.Save(_folderAddress + "\\accounts.xml");
        }
    }
}
