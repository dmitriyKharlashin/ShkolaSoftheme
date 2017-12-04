using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MakeCallEventArgs : EventArgs
    {

        public MakeCallEventArgs(string receiverNumber)
        {
            ReceiverNumber = receiverNumber;
        }

        public string ReceiverNumber { get; private set; }
    }
}
