using FluentAssertions;
using Maplink.DesafioDev.WebApi.Lib;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Maplink.DesafioDev.WebApi.Tests.Lib
{
    [TestFixture]
    public class CustomSerializerSettingsTests
    {
        [Test]
        public void CustomSerializerSettings_WhenSerializeObject_ShouldSetCamelCasePattern()
        {
            var stub = new Stub
            {
                Name = "attribute",
                Value = "first"
            };

            const string expected = @"{""name"":""attribute"",""value"":""first""}";

            var customSerializerSettings = new CustomSerializerSettings();
            var json = JsonConvert.SerializeObject(stub, customSerializerSettings);

            json
                .Should()
                .Be(expected);
        }

        class Stub
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}