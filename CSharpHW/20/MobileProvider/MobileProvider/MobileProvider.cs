using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

                account.SendSmsProcessing += ProvideMessageConnection;
                account.MakeCallProcessing += ProvideCallConnection;
            //}
        }

        [AdminValidation]
        private void ProvideMessageConnection(object sender, ConnectionEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account != null && !e.ReceiverNumber.Equals(null))
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
            else if (account != null && e.ReceiverNumber.Equals(null) && Validator.TryValidateObject(sender, new ValidationContext(sender), new List<ValidationResult>()))
            {
                foreach (var mobileAccount in Accounts)
                {
                    LoggerStatusTypes messageStatus = LoggerStatusTypes.Success;
                    mobileAccount.ReceiveSms(e.Message, account.Number);
                    DeliveringSmsAction?.Invoke(messageStatus, account.Number, mobileAccount.Number);
                }
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor) LoggerColorTypes.Alert;
                Console.WriteLine("Your account can`t connect with the recipient");
                Console.ResetColor();
            }

            
        }

        private void ProvideCallConnection(object sender, ConnectionEventArgs e)
        {
            var account = sender as MobileAccount;
            if (account != null && !e.ReceiverNumber.Equals(null))
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
