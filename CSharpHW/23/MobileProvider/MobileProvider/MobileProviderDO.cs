using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MobileProvider
{
    [Serializable]
    [XmlInclude(typeof(IMobileProvider))]
    [XmlInclude(typeof(Provider))]
    [XmlRoot(ElementName = "MobileProvider")]
    public class MobileProviderDO
    {
        public string Name { get; set; }

        public List<MobileAccountDO> Accounts { get; set; }

        public MobileProviderDO()
        {

        }
    }
}
