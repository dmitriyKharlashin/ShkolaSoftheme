using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    public class ConnectionEventArgs : EventArgs
    {

        public int ReceiverNumber { get; private set; }

        public string Message { get; private set; }

        public ConnectionEventArgs(int receiverNumber)
        {
            ReceiverNumber = receiverNumber;
        }

        public ConnectionEventArgs(string message)
        {
            Message = message;
        }

        public ConnectionEventArgs(string message, int receiverNumber)
        {
        
            Message = message;
            ReceiverNumber = receiverNumber;
        }
    }
}
