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
                ILogger eventLogger = new EventLogger();
                //ILogger callLogger = new CallLogger();

                // subscribe loggers into provider events
                mobileProviderVodaphone.DeliveringSmsAction += eventLogger.GetMessageEvent;
                mobileProviderVodaphone.DeliveringCallAction += eventLogger.GetCallEvent;

                // add accounts
                mobileProviderVodaphone.AddAccount("0951111111");
                mobileProviderVodaphone.AddAccount("0961111111");
                mobileProviderVodaphone.AddAccount("0501111111");
                mobileProviderVodaphone.AddAccount("0731111111");
                mobileProviderVodaphone.AddAccount("0661111111");
                mobileProviderVodaphone.AddAccount("0671111111");
                mobileProviderVodaphone.AddAccount("0951111111");

                // try to find number in mobile provider account
                IMobileAccount mobileAccount1 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("0501111111"));
                IMobileAccount mobileAccount2 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("0951111111"));
                IMobileAccount mobileAccount3 = mobileProviderVodaphone.Accounts.FirstOrDefault(p => p.Number.Equals("0661111111"));

                mobileAccount1?.AddAddress(new Dictionary<string, string>(){
                    {"0671111111", "Allain"},
                    {"0961111111", "Edgar"},
                    {"0661111111", "Milan"},
                });
                mobileAccount2?.AddAddress("0501111111", "Albert");
                mobileAccount3?.AddAddress(new Dictionary<string, string>(){
                    {"0671111111", "Marge"},
                    {"0731111111", "Vins"},
                    {"0951111111", "Robert"},
                });

                // successful deliverying
                mobileAccount1?.SendSms("Hello World", "0951111111");
                // unsuccessful deliverying
                mobileAccount1?.SendSms("Hello World", "0951111112");
                // successfull call
                mobileAccount1?.MakeACall("0671111111");
                mobileAccount1?.MakeACall("0961111111");

                Console.WriteLine();
                // change mobile account
                // successful deliverying
                mobileAccount2?.SendSms("Hello Another World", "0661111111");
                mobileAccount2?.SendSms("Connect World", "0661111111");

                // successfull call
                mobileAccount2?.MakeACall("0731111111");
                // unsucsessfull call
                mobileAccount2?.MakeACall("0991111111");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
