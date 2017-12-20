using System.IO;
using ProtoBuf;

namespace MobileProvider
{
    public class ProtoBufParser : IParser
    {
        public void ParseFromFile<TDataType>(string fileName, out TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                fs.Position = 0;
                data = (TDataType)Serializer.Deserialize<TDataType>(fs);
            }
        }

        public void ParseIntoFile<TDataType>(string fileName, TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                fs.Position = 0;
                Serializer.Serialize<TDataType>(fs, data);
            }
        }
    }
}