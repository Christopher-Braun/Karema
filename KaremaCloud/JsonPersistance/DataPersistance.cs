
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using KaReMa.Interfaces;

namespace Mvc4WebRole
{
    public class DataPersistance
    {
        public static Stream SaveData(DataCollection data)
        {
            var serializer = new DataContractSerializer(typeof(DataCollection), new DataContractSerializerSettings()
            {
                DataContractResolver = new ProxyDataContractResolver()
            });

            Stream stream = new MemoryStream();

            serializer.WriteObject(stream, data);

            return stream;
        }
    }
}