using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MobileAccount : IMobileAccount
    {
        public event EventHandler<MakeMessagingEventArgs> SendSmsProcessingComplete;
        public event EventHandler<MakeCallEventArgs> MakeCallProcessingStart;

        private readonly Dictionary<string, string> _addresses = new Dictionary<string, string>();

        public string Number { get; }

        private Dictionary<string, string> Addresses
        {
            get { return _addresses; }
        }

        public MobileAccount(string number)
        {
            Number = number;
        }

        public void AddAddress(string number, string name)
        {
            _addresses.Add(number, name);
        }
        public void AddAddress(Dictionary<string, string> items)
        {
            foreach(KeyValuePair<string, string> item in items)
            {
                AddAddress(item.Key, item.Value);
            }
        }

        public void SendSms(string message, string receiver)
        {
            SendSmsProcessingComplete?.Invoke(this, new MakeMessagingEventArgs(message, receiver));
        }

        public void ReceiveSms(string message, string sender)
        {
            if (IsConnectionValid(sender))
            {
                string senderName = GetSenderNameFromAddressBook(sender);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Message from {sender}({senderName}) - \"{message}\"");
                Console.ResetColor();
            }
        }
        
        public void MakeACall(string receiver)
        {
            MakeCallProcessingStart?.Invoke(this, new MakeCallEventArgs(receiver));
        }

        public void ReceiveCall(string caller)
        {
            if (IsConnectionValid(caller))
            {
                string senderName = GetSenderNameFromAddressBook(caller);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Call from {caller}({senderName})");
                Console.ResetColor();
            }
        }

        private string GetSenderNameFromAddressBook(string phoneNumber)
        {
            return Addresses.FirstOrDefault(p => p.Key == phoneNumber).Value;
        }

        private bool IsConnectionValid(string phoneNumber)
        {
            return Addresses.ContainsKey(phoneNumber);
        }
    }
}
