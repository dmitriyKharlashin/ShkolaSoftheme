using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MakeCallEventArgs<T> : EventArgs
    {

        public MakeCallEventArgs(T receiverNumber)
        {
            ReceiverNumber = receiverNumber;
        }

        public T ReceiverNumber { get; private set; }
    }
}
