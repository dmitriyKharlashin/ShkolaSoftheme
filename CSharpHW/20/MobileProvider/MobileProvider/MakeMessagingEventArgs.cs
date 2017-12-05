using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MakeMessagingEventArgs : EventArgs
    {
        public MakeMessagingEventArgs(string message, int receiverNumber)
        {
            ReceiverNumber = receiverNumber;
            Message = message;
        }

        public int ReceiverNumber { get; private set; }

        public string Message { get; private set; }
    }
}
