using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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

                // add accounts
                mobileProviderVodaphone.AddAccount("1");
                mobileProviderVodaphone.AddAccount("2");
                mobileProviderVodaphone.AddAccount("3");
                mobileProviderVodaphone.AddAccount("4");
                mobileProviderVodaphone.AddAccount("5");
                mobileProviderVodaphone.AddAccount("6");
                mobileProviderVodaphone.AddAccount("7");
                mobileProviderVodaphone.AddAccount("2");


                // try to find number in mobile provider account
                IMobileAccount mobileAccount1 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("1"));
                IMobileAccount mobileAccount2 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("2"));
                IMobileAccount mobileAccount3 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("3"));

                mobileAccount1?.AddAddress(new Dictionary<string, string>(){
                    {"7", "Allain"},
                    {"6", "Edgar"},
                    {"2", "Milan"},
                });
                mobileAccount2?.AddAddress("1", "Albert");
                mobileAccount3?.AddAddress(new Dictionary<string, string>(){
                    {"2", "Marge"},
                    {"1", "Vins"},
                    {"5", "Robert"},
                });
                
                mobileAccount1?.SendSms("Hello World", "2");
                mobileAccount1?.SendSms("Hello World", "3");
                mobileAccount1?.MakeACall("2");
                mobileAccount1?.MakeACall("7");

                Console.WriteLine();
                // change mobile account
                mobileAccount2?.SendSms("Hello Another World", "5");
                mobileAccount2?.SendSms("Connect World", "3");

                mobileAccount2?.MakeACall("1");
                mobileAccount2?.MakeACall("3");
                mobileAccount2?.MakeACall("5");

                mobileAccount3?.MakeACall("1");
                mobileAccount3?.MakeACall("2");
                mobileAccount3?.MakeACall("5");
                mobileAccount3?.SendSms("Hello World", "2");
                mobileAccount3?.SendSms("Hello World", "6");
                mobileAccount3?.MakeACall("2");
                mobileAccount3?.MakeACall("7");

                // show rates
                callLogger.ShowTopSenderList();
                callLogger.ShowTopRecieverList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
