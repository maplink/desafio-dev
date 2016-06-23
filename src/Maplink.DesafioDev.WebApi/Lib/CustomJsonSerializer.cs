using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Maplink.DesafioDev.WebApi.Lib
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            Initialize();
        }

        private void Initialize()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            NullValueHandling = NullValueHandling.Ignore;

            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = true,
                CamelCaseText = true
            });
        }
    }
}