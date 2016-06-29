using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maplink.DesafioDev.WebApi.Lib
{
    public class CustomSerializerSettings : JsonSerializerSettings
    {
        public CustomSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}