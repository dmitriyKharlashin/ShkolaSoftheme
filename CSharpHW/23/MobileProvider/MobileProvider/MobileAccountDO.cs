using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProtoBuf;

namespace MobileProvider
{
    [Serializable]
    [XmlInclude(typeof(MobileAccountDO))]
    [XmlInclude(typeof(List<MobileAccountDO>))]
    [XmlRoot(ElementName = "MobileAccount")]
    [SoapInclude(typeof(List<MobileAccountDO>))]
    [SoapInclude(typeof(MobileAccountDO))]
    [DataContract]
    [KnownType(typeof(List<MobileAccountDO>))]
    [KnownType(typeof(MobileAccountDO))]
    [ProtoContract]
    public class MobileAccountDO
    {
        [DataMember]
        [ProtoMember(1)]
        public int Number { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Name { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public string Surname { get; set; }

        [DataMember]
        [ProtoMember(4)]
        public string Email { get; set; }

        [DataMember]
        [ProtoMember(5)]
        public int BirthYear { get; set; }

        [DataMember]
        [ProtoMember(6)]
        public List<MobileAddress> Addresses { get; set; }

        public MobileAccountDO()
        {

        }
    }
}
