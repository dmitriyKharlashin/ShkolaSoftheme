using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace MobileProvider
{
    public class JsonParser<TDataType> : IParser
    {
        public readonly DataContractJsonSerializer Serializer;

        public JsonParser()
        {
            Serializer = new DataContractJsonSerializer(typeof(TDataType));
        }

        public void ParseFromFile<TDataType>(string fileName, out TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                data = (TDataType)Serializer.ReadObject(fs);
            }
        }

        public void ParseIntoFile<TDataType>(string fileName, TDataType data)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                Serializer.WriteObject(fs, data);
            }
        }
    }
}