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
            if (!IsNumberServiced(account))
            {
                Accounts.Add(account);

                account.SendSmsProcessingComplete += ProvideConnection;
                account.MakeCallProcessingStart += ProvideConnection;
            }
        }

        public void AddAccount(int phoneNumber)
        {
            MobileAccount mobileAccount = new MobileAccount(phoneNumber);
            AddAccount(mobileAccount);
        }

        private void ProvideConnection(object sender, MakeMessagingEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account == null)
            {
                throw new ArgumentException();
            }

            IMobileAccount receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(account.Number));
            int messageStatus = (int)LoggerStatusTypes.Error;
            if (receiver != null)
            {
                messageStatus = (int) LoggerStatusTypes.Success;
                receiver.ReceiveSms(e.Message, account.Number);
            }
            DeliveringSmsAction?.Invoke(messageStatus, account.Number, e.ReceiverNumber);
        }

        private void ProvideConnection(object sender, MakeCallEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account == null)
            {
                throw new ArgumentException();
            }

            IMobileAccount receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(account.Number));
            int messageStatus = (int)LoggerStatusTypes.Error;
            if (receiver != null)
            {
                messageStatus = (int) LoggerStatusTypes.Success;
                receiver.ReceiveCall(account.Number);
            }
            DeliveringCallAction?.Invoke(messageStatus, account.Number, e.ReceiverNumber);
        }
    }
}
