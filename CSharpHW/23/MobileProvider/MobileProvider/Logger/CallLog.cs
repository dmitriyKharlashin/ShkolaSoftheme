using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    [Serializable]
    public class CallLog
    {
        public LoggerStatusTypes Status { get; }

        public ActivityTypes ActivityType { get; }

        public int Sender { get; }

        public int Reciever { get; }

        public CallLog(LoggerStatusTypes status, int sender, int reciever, ActivityTypes activityType)
        {
            Status = status;
            Sender = sender;
            Reciever = reciever;
            ActivityType = activityType;
        }
    }
}
