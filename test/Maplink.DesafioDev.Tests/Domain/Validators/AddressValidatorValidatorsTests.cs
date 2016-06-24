using System.Linq;
using FluentAssertions;
using FluentValidation;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Domain.Validators;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Validators
{
    public class AddressValidatorValidatorsTests
    {
        private IValidator _validator;
        private Address _address;

        [SetUp]
        public void SetUp()
        {
            _validator = new AddressValidator();

            _address = new Address
            {
                City = "city",
                Number = "number",
                State = "sp",
                Street = "street"
            };
        }

        [Test]
        public void Validate_GivenValidAdderss_ShouldNotHaveErrors()
        {
            _validator
                .Validate(_address)
                .Errors
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Validate_GivenEmptyStreet_ShouldContainExpectedErrorMessage()
        {
            _address.Street = null;

            _validator
                .Validate(_address)
                .Errors
                .Any(p => p.ErrorMessage == "street information should be informed")
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validate_GivenEmptyNumber_ShouldContainExpectedErrorMessage()
        {
            _address.Number = null;

            _validator
                .Validate(_address)
                .Errors
                .Any(p => p.ErrorMessage == "number information should be informed")
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validate_GivenEmptyCity_ShouldContainExpectedErrorMessage()
        {
            _address.City = null;

            _validator
                .Validate(_address)
                .Errors
                .Any(p => p.ErrorMessage == "city information should be informed")
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validate_GivenEmptyState_ShouldContainExpectedErrorMessage()
        {
            _address.State = null;

            _validator
                .Validate(_address)
                .Errors
                .Any(p => p.ErrorMessage == "state information should be informed")
                .Should()
                .BeTrue();
        }

        [Test]
        public void Validate_GivenInvalidState_ShouldContainExpectedErrorMessage()
        {
            _address.State = "RE";

            _validator
                .Validate(_address)
                .Errors
                .Any(p => p.ErrorMessage == "entry state information is not valid")
                .Should()
                .BeTrue();
        }
    }
}