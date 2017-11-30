using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class Provider<T> : IMobileProvider<T>
    {
        private HashSet<IMobileAccount<T>> _accounts;
        
        public HashSet<IMobileAccount<T>> Accounts {
            get {
                return _accounts;
            }
        }

        public string Name { get; set; }

        public Provider(string name)
        {
            _accounts = new HashSet<IMobileAccount<T>>();
            Name = name;
        }
        
        public void AddAccount(T mobile)
        {
            MobileAccount<T> mobileAccount = new MobileAccount<T>(mobile);
            _accounts.Add(mobileAccount);
            
            mobileAccount.SendSMSProcessingComplete += ProvideConnection;
            mobileAccount.MakeCallProcessingStart += ProvideConnection;
        }

        private void ProvideConnection(object s, MakeMessagingEventArgs<T> e)
        {
            MobileAccount<T> sender = s as MobileAccount<T>;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Provider is trying to sent message to receiver...");
            IMobileAccount<T> receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber));
            if (receiver != null && !receiver.Equals("") && !e.ReceiverNumber.Equals(sender.Number))
            {
                Console.WriteLine("Message: from {0} to {1} was successfully sent", sender.Number, receiver.Number);
                receiver.ReceiveSMS(e.Message, sender.Number);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Message: from {0} to {1} was not delivered!", sender.Number, e.ReceiverNumber);
            }
            Console.ResetColor();
        }

        private void ProvideConnection(object s, MakeCallEventArgs<T> e)
        {
            MobileAccount<T> sender = s as MobileAccount<T>;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Provider is trying to connect caller with receiver...");
            IMobileAccount<T> receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber));
            if (receiver != null && !receiver.Equals("") && !e.ReceiverNumber.Equals(sender.Number))
            {
                Console.WriteLine("Call: from {0} to {1} was successfully started", sender.Number, receiver.Number);
                receiver.ReceiveCall(sender.Number);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Call: from {0} to {1} was not started!", sender.Number, e.ReceiverNumber);
            }
            Console.ResetColor();
        }
    }
}
