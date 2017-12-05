using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class Provider : IMobileProvider
    {
        public event Action<int, int, int> DeliveringSmsAction;
        public event Action<int, int, int> DeliveringCallAction;

        private readonly List<IMobileAccount> _accounts = new List<IMobileAccount>();

        public string Name { get; set; }

        public ICollection<IMobileAccount> Accounts
        {
            get { return _accounts; }
        }

        public Provider(string name)
        {
            Name = name;
        }

        private bool IsNumberServiced(IMobileAccount account)
        {
            return Accounts.Any(p => p.Number == account.Number);
        }

        public void AddAccount(IMobileAccount account)
        {
            //if (!IsNumberServiced(account))
            //{
                account.Number = GeneratePhoneNumber();
                Accounts.Add(account);

                account.SendSmsProcessingComplete += ProvideConnection;
                account.MakeCallProcessingStart += ProvideConnection;
            //}
        }
        
        private void ProvideConnection(object s, MakeMessagingEventArgs e)
        {
            if (!(s is MobileAccount sender))
            {
                throw new ArgumentException();
            }

            IMobileAccount receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(sender.Number));
            int messageStatus = (int)LoggerStatusTypes.Error;
            if (receiver != null)
            {
                messageStatus = (int) LoggerStatusTypes.Success;
                receiver.ReceiveSms(e.Message, sender.Number);
            }
            DeliveringSmsAction?.Invoke(messageStatus, sender.Number, e.ReceiverNumber);
        }

        private void ProvideConnection(object s, MakeCallEventArgs e)
        {
            if (!(s is MobileAccount sender))
            {
                throw new ArgumentException();
            }

            IMobileAccount receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(sender.Number));
            int messageStatus = (int)LoggerStatusTypes.Error;
            if (receiver != null)
            {
                messageStatus = (int) LoggerStatusTypes.Success;
                receiver.ReceiveCall(sender.Number);
            }
            DeliveringCallAction?.Invoke(messageStatus, sender.Number, e.ReceiverNumber);
        }

        private int GeneratePhoneNumber()
        {
            IMobileAccount lastAccount = Accounts.LastOrDefault();
            int? number = lastAccount?.Number;
            return number + 1 ?? 1;
        }
    }
}
