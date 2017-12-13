using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class SoapParser : IParser
    {
        private SoapFormatter _serializer;

        public SoapParser()
        {
            _serializer = new SoapFormatter();
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
