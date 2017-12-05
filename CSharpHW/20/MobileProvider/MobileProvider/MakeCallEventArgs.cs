using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class MakeCallEventArgs : EventArgs
    {

        public MakeCallEventArgs(int receiverNumber)
        {
            ReceiverNumber = receiverNumber;
        }

        public int ReceiverNumber { get; private set; }
    }
}
