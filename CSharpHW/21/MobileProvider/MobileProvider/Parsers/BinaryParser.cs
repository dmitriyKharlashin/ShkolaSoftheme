using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MobileProvider
{
    class BinaryParser : IParser
    {
        BinaryFormatter _serializer = new BinaryFormatter();

        public void ParseFromFile<TDataType>(string fileName, out TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
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
