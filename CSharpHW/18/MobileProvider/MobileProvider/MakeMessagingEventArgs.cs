using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MakeMessagingEventArgs<T> : EventArgs
    {
        public MakeMessagingEventArgs(string message, T receiverNumber)
        {
            ReceiverNumber = receiverNumber;
            Message = message;
        }

        public T ReceiverNumber { get; private set; }

        public string Message { get; private set; }
    }
}
