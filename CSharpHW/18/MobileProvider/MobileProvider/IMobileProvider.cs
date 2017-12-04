using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    interface IMobileProvider<T>
    {
        string Name { get; set; }

        HashSet<IMobileAccount<T>> Accounts { get; }

        void AddAccount(T mobile);
    }
}
