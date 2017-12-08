using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    [AdminValidation]
    class AdminAccount : MobileAccount
    {
        public event EventHandler<ConnectionEventArgs> SendSmsProcessing;

        public AdminAccount(string name, string surname, string email, int birthYear) : base(name, surname, email, birthYear)
        {
            Role = UserRoles.Admin;
        }

        public void SendMessageToAll(string message)
        {
            SendSmsProcessing?.Invoke(this, new ConnectionEventArgs(message));
        }
        
    }
}
