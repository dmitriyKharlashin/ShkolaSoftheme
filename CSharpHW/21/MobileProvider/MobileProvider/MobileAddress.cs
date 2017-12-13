using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace MobileProvider
{
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class MobileAddress
    {
        [DataMember]
        [ProtoMember(1)]
        public int Number { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Name { get; set; }

        public MobileAddress()
        {
            
        }
    }
}
