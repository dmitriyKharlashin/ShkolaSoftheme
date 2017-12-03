using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    internal interface IMobileProvider
    {
        event Action<int, string, string> DeliveringSmsAction;
        event Action<int, string, string> DeliveringCallAction;

        string Name { get; set; }

        ICollection<IMobileAccount> Accounts { get; }

        void AddAccount(string mobile);
    }
}
