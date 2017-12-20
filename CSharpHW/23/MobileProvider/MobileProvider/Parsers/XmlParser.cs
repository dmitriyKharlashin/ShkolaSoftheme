using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MobileProvider
{
    class XmlParser<TDataType> : IParser
    {
        private XmlSerializer _serializer;

        public XmlParser()
        {
            _serializer = new XmlSerializer(typeof(TDataType));
        }

        public void ParseFromFile<TDataType>(string fileName, out TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                fs.Position = 0;
                data = (TDataType)_serializer.Deserialize(fs);
            }
        }

        public void ParseIntoFile<TDataType>(string fileName, TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                fs.Position = 0;
                _serializer.Serialize(fs, data);
            }
        }
    }
}
