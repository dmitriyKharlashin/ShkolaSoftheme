using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    internal interface IMobileProvider
    {
        event Action<LoggerStatusTypes, int, int> DeliveringSmsAction;
        event Action<LoggerStatusTypes, int, int> DeliveringCallAction;

        string Name { get; set; }

        List<MobileAccount> Accounts { get; }

        void AddAccount(IMobileAccount account);
    }
}
