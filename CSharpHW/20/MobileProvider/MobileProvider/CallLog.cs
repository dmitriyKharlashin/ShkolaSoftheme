using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    public class CallLog
    {
        public LoggerStatusTypes Status { get; }

        public ConnectionTypes ConnectionType { get; }

        public int Sender { get; }

        public int Reciever { get; }

        public CallLog(LoggerStatusTypes status, int sender, int reciever, ConnectionTypes connection_type)
        {
            Status = status;
            Sender = sender;
            Reciever = reciever;
            ConnectionType = connection_type;
        }
    }
}
