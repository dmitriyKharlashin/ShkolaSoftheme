using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class Provider : IMobileProvider
    {
        public event Action<LoggerStatusTypes, int, int> DeliveringSmsAction;
        public event Action<LoggerStatusTypes, int, int> DeliveringCallAction;

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

        private void ProvideConnection(object sender, MakeMessagingEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account != null)
            {
                IMobileAccount receiver = Accounts.FirstOrDefault(p =>
                    p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(account.Number));
                LoggerStatusTypes messageStatus = LoggerStatusTypes.Error;
                if (receiver != null)
                {
                    messageStatus = LoggerStatusTypes.Success;
                    receiver.ReceiveSms(e.Message, account.Number);
                }
                DeliveringSmsAction?.Invoke(messageStatus, account.Number, e.ReceiverNumber);
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor) LoggerColorTypes.Alert;
                Console.WriteLine("Your account can`t connect with the recipient");
                Console.ResetColor();
            }

            
        }

        private void ProvideConnection(object sender, MakeCallEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account != null)
            {
                IMobileAccount receiver = Accounts.FirstOrDefault(p => p.Number.Equals(e.ReceiverNumber) && !p.Number.Equals(account.Number));
                LoggerStatusTypes messageStatus = LoggerStatusTypes.Error;
                if (receiver != null)
                {
                    messageStatus = LoggerStatusTypes.Success;
                    receiver.ReceiveCall(account.Number);
                }
                DeliveringCallAction?.Invoke(messageStatus, account.Number, e.ReceiverNumber);
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)LoggerColorTypes.Alert;
                Console.WriteLine("Your account can`t connect with the recipient");
                Console.ResetColor();
            }
        }

        private int GeneratePhoneNumber()
        {
            IMobileAccount lastAccount = Accounts.LastOrDefault();
            int? number = lastAccount?.Number;
            return number + 1 ?? 0951000001;
        }
    }
}
