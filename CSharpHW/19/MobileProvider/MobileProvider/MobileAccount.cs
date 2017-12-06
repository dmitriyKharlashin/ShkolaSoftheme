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

        private readonly Dictionary<int, string> _addresses = new Dictionary<int, string>();

        public int Number { get; }

        private Dictionary<int, string> Addresses
        {
            get { return _addresses; }
        }

        public MobileAccount(int number)
        {
            Number = number;
        }

        public void AddAddress(int number, string name)
        {
            _addresses.Add(number, name);
        }
        public void AddAddress(Dictionary<int, string> items)
        {
            foreach(KeyValuePair<int, string> item in items)
            {
                AddAddress(item.Key, item.Value);
            }
        }

        public void SendSms(string message, int receiver)
        {
            SendSmsProcessingComplete?.Invoke(this, new MakeMessagingEventArgs(message, receiver));
        }

        public void ReceiveSms(string message, int sender)
        {
            if (IsConnectionValid(sender))
            {
                string senderName = GetSenderNameFromAddressBook(sender);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Message from {sender}({senderName}) - \"{message}\"");
                Console.ResetColor();

                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Account {Number}: Message from non-whitelisted account: {sender} - \"{message}\"");
            Console.ResetColor();
        }
        
        public void MakeACall(int receiver)
        {
            MakeCallProcessingStart?.Invoke(this, new MakeCallEventArgs(receiver));
        }

        public void ReceiveCall(int caller)
        {
            if (IsConnectionValid(caller))
            {
                string senderName = GetSenderNameFromAddressBook(caller);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Account {Number}: Call from {caller}({senderName})");
                Console.ResetColor();

                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Account {Number}: Call from non-whitelisted account {caller}");
            Console.ResetColor();
        }

        private string GetSenderNameFromAddressBook(int phoneNumber)
        {
            return Addresses.FirstOrDefault(p => p.Key == phoneNumber).Value;
        }

        private bool IsConnectionValid(int phoneNumber)
        {
            return Addresses.ContainsKey(phoneNumber);
        }
    }
}
