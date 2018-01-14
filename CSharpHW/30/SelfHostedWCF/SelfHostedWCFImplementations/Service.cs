using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfHostedWCF;

namespace SelfHostedWCFImplementations
{
    public class Service : IService
    {
        public string EchoWithGet(string value)
        {
            return $"You said: {value}";
        }

        public string EchoWithPost(string value)
        {
            return $"You said: {value}";
        }
    }
}
