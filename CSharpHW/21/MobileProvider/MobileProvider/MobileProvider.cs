using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;

namespace MobileProvider
{
    class Provider : IMobileProvider
    {
        public event Action<LoggerStatusTypes, int, int> DeliveringSmsAction;
        public event Action<LoggerStatusTypes, int, int> DeliveringCallAction;
        
        private readonly List<MobileAccount> _accounts = new List<MobileAccount>();

        public string Name { get; set; }

        public List<MobileAccount> Accounts
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
            var isAdmin = account.GetType().IsDefined(typeof(AdminValidationAttribute), false);
            //if (!IsNumberServiced(account))
            //{
                account.Number = GeneratePhoneNumber();
                Accounts.Add(account as MobileAccount);

                account.SendSmsProcessing += ProvideMessageConnection;
                account.MakeCallProcessing += ProvideCallConnection;
                if (isAdmin)
                {
                    AdminAccount adminAccount = account as AdminAccount;
                    if (adminAccount != null) adminAccount.SendSmsToAll += ProvideAdminNotifications;
                }

            //}
        }

        public MobileProviderDO ConvertIntoDO()
        {
            return new MobileProviderDO()
            {
                Name = Name,
                Accounts = (List<MobileAccountDO>)Accounts.Select(p => p.ConvertIntoDO()).ToList()
            };
        }

        private void ProvideAdminNotifications(object sender, ConnectionEventArgs e)
        {
            var account = sender as AdminAccount;
            if (account != null)
            {
                IEnumerable<IMobileAccount> receivers = Accounts.Where(p => !p.Number.Equals(account.Number));

                foreach (var receiver in receivers)
                {
                    LoggerStatusTypes messageStatus = LoggerStatusTypes.Error;
                    messageStatus = LoggerStatusTypes.Success;
                    receiver.ReceiveSms(e.Message, account.Number);
                    DeliveringSmsAction?.Invoke(messageStatus, account.Number, e.ReceiverNumber);
                }
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)LoggerColorTypes.Alert;
                Console.WriteLine("Your account can`t connect with the recipient");
                Console.ResetColor();
            }
        }

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
