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

        public ActivityTypes ActivityType { get; }

        public int Sender { get; }

        public int Reciever { get; }

        public CallLog(LoggerStatusTypes status, int sender, int reciever, ActivityTypes activity_type)
        {
            Status = status;
            Sender = sender;
            Reciever = reciever;
            ActivityType = activity_type;
        }
    }
}
