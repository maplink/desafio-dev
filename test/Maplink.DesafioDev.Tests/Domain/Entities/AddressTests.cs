using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Entities
{
    [TestFixture]
    public class AddressTests
    {
        private Address _address;

        [SetUp]
        public void SetUp()
        {
            _address = new Address
            {
                State = "SP",
                City = "Sao Paulo",
                Number = "2000",
                Street = "Av Paulista"
            };
        }

        [TestCase("AC")]
        [TestCase("AL")]
        [TestCase("AP")]
        [TestCase("AM")]
        [TestCase("BA")]
        [TestCase("CE")]
        [TestCase("DF")]
        [TestCase("ES")]
        [TestCase("GO")]
        [TestCase("MA")]
        [TestCase("MT")]
        [TestCase("MS")]
        [TestCase("MG")]
        [TestCase("PA")]
        [TestCase("PB")]
        [TestCase("PR")]
        [TestCase("PE")]
        [TestCase("PI")]
        [TestCase("RJ")]
        [TestCase("RN")]
        [TestCase("RS")]
        [TestCase("RO")]
        [TestCase("RR")]
        [TestCase("SC")]
        [TestCase("SP")]
        [TestCase("SE")]
        [TestCase("TO")]
        public void IsValidState_GivenValidState_ShouldBeTrue(string state)
        {
            _address.State = state;

            _address
                .IsValidState()
                .Should()
                .BeTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("AA")]
        public void IsValidState_GivenInvalidState_ShouldBeFalse(string state)
        {
            _address.State = state;

            _address
                .IsValidState()
                .Should()
                .BeFalse();
        }

        [Test]
        public void Toautocomplete_GivenValidAddress_ShouldConvertToExpectedValue()
        {
            _address
                .ToAutocomplete()
                .Should()
                .Be("Av Paulista, 2000, Sao Paulo, SP");
        }
    }
}