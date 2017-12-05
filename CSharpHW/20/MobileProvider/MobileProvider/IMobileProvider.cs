using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    internal interface IMobileProvider
    {
        event Action<int, int, int> DeliveringSmsAction;
        event Action<int, int, int> DeliveringCallAction;

        string Name { get; set; }

        ICollection<IMobileAccount> Accounts { get; }

        void AddAccount(IMobileAccount account);
    }
}
