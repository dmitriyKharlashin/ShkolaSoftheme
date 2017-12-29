using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileProvider
{
    class JsonConvertParser<TDataType> : IParser
    {
        //public readonly JsonConvert Serializer;

        public JsonConvertParser()
        {
            //Serializer = new JsonConvert();
        }

        public void ParseFromFile<TDataType>(string fileName, out TDataType data)
        {
            data = default(TDataType);

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    if (fs.Length > 0)
                    {
                        data = JsonConvert.DeserializeObject<TDataType>(sr.ReadToEnd());
                    }
                }
            }
        }

        public void ParseIntoFile<TDataType>(string fileName, TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    var json = JsonConvert.SerializeObject(data);
                    sw.Write(json);
                }
            }
        }
    }
}