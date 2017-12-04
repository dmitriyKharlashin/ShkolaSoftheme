using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MobileAccount<T> : IMobileAccount<T>
    {
        public event EventHandler<MakeMessagingEventArgs<T>> SendSMSProcessingComplete;
        public event EventHandler<MakeCallEventArgs<T>> MakeCallProcessingStart;

        private T _number;
        private Dictionary<T, string> _addresses = new Dictionary<T, string>();

        public T Number {
            get
            {
                return _number;
            }
        }

        private Dictionary<T, string> Addresses
        {
            get {
                return _addresses;
            }
        }

        public MobileAccount(T number)
        {
            _number = number;
        }

        public void AddAddress(T number, string name)
        {
            _addresses.Add(number, name);
        }
        public void AddAddress(Dictionary<T, string> items)
        {
            foreach(KeyValuePair<T, string> item in items)
            {
                AddAddress(item.Key, item.Value);
            }
        }

        public void SendSMS(string message, T receiver)
        {
            if(SendSMSProcessingComplete != null)
            {
                SendSMSProcessingComplete.Invoke(this, new MakeMessagingEventArgs<T>(message, receiver));
            }
        }
        public void ReceiveSMS(string message, T sender)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string senderName = GetSenderNameFromAddressBook(sender);
            string senderNameTemplate = (senderName == null ? "" : "(" + senderName + ")");
            Console.WriteLine("Account {0}: Message from {1}{3} - \"{2}\"", Number, sender, message, senderNameTemplate);
        }

        public void MakeACall(T receiver)
        {
            if (MakeCallProcessingStart != null)
            {
                MakeCallProcessingStart.Invoke(this, new MakeCallEventArgs<T>(receiver));
            }
        }

        public void ReceiveCall(T caller)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string senderName = GetSenderNameFromAddressBook(caller);
            string senderNameTemplate = (senderName == null ? "" : "(" + senderName + ")");
            Console.WriteLine("Account {0}: Call with {1}{2}", Number, caller, senderNameTemplate);
            Console.ResetColor();
        }

        private string GetSenderNameFromAddressBook(T number)
        {
            if (Addresses.ContainsKey(number))
            {
                return _addresses[number];
            }

            return null;
        }
    }
}
