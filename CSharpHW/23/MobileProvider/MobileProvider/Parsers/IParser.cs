using System.Collections.Generic;

namespace MobileProvider
{
    interface IParser
    {

        void ParseIntoFile<TDataType>(string fileName, TDataType data);

        void ParseFromFile<TDataType>(string fileName, out TDataType data);
    }
}