using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    interface IMobileAccount<T>
    {
        T Number { get; }

        void AddAddress(T number, string name);

        void AddAddress(Dictionary<T, string> args);
            
        void ReceiveSMS(string message, T sender);

        void SendSMS(string message, T receiver);

        void MakeACall(T receiver);

        void ReceiveCall(T caller);
    }
}
