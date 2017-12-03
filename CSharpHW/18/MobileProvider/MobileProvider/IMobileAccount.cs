using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    interface IMobileAccount
    {
        string Number { get; }

        void AddAddress(string number, string name);

        void AddAddress(Dictionary<string, string> args);
            
        void ReceiveSms(string message, string sender);

        void SendSms(string message, string receiver);

        void MakeACall(string receiver);

        void ReceiveCall(string caller);
    }
}
