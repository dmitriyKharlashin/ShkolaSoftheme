using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    public interface IMobileAccount
    {
        event EventHandler<ConnectionEventArgs> SendSmsProcessing;
        event EventHandler<ConnectionEventArgs> MakeCallProcessing;
        
        string Name { get; }

        string Surname { get; }

        string Email { get; }

        int BirthYear { get; }

        int Number { get; set; }
        
        List<MobileAddress> Addresses { get; }

        void AddAddress(int number, string name);

        void AddAddress(Dictionary<int, string> args);
            
        void ReceiveSms(string message, int sender);

        void SendSms(string message, int receiver);

        void MakeACall(int receiver);

        void ReceiveCall(int caller);
    }
}
