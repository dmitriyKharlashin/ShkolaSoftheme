using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    interface IMobileAccount
    {
        event EventHandler<MakeMessagingEventArgs> SendSmsProcessingComplete;
        event EventHandler<MakeCallEventArgs> MakeCallProcessingStart;

        int Number { get; }

        void AddAddress(int number, string name);

        void AddAddress(Dictionary<int, string> args);
            
        void ReceiveSms(string message, int sender);

        void SendSms(string message, int receiver);

        void MakeACall(int receiver);

        void ReceiveCall(int caller);
    }
}
