using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    interface IMobileAccount
    {
        event EventHandler<ConnectionEventArgs> SendSmsProcessing;
        event EventHandler<ConnectionEventArgs> MakeCallProcessing;

        int Number { get; set; }

        void AddAddress(int number, string name);

        void AddAddress(Dictionary<int, string> args);
            
        void ReceiveSms(string message, int sender);

        void SendSms(string message, int receiver);

        void MakeACall(int receiver);

        void ReceiveCall(int caller);
    }
}
