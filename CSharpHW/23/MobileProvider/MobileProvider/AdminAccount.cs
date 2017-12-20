using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MobileProvider
{
    public class AdminAccount : MobileAccount
    {
        public event EventHandler<ConnectionEventArgs> SendSmsToAll;

        public AdminAccount(string name, string surname, string email, int birthYear) : base(name, surname, email, birthYear)
        {

        }

        public void SendMessageToAll(string message)
        {
            SendSmsToAll?.Invoke(this, new ConnectionEventArgs(message));
        }
        
    }
}
