using System.Linq;
using FluentAssertions;
using FluentValidation;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Domain.Validators;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Validators
{
    [TestFixture]
    public class RouteRequestValidatorTests
    {
        private IValidator _validator;
        private RouteRequest _routeRequest;

        [SetUp]
        public void SetUp()
        {
            _validator = new RouteRequestValidator();

            _routeRequest = new RouteRequest
            {
                Addresses = new[]
                {
                    new Address
                    {
                        City = "city",
                        Number = "number",
                        State = "sp",
                        Street = "street"
                    }
                },
                Type = "shortest"
            };
        }

        [Test]
        public void Validate_GivenValidRouteRequest_ShouldNotHaveErrors()
        {
            _validator
                .Validate(_routeRequest)
                .Errors
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Validate_GivenEmptyAddress_ShouldContainExpectedErrorMessage()
        {
            _routeRequest.Addresses = null;

            _validator
                .Validate(_routeRequest)    
                .Errors
                .Any(p => p.ErrorMessage == "addresses should be informed")
                .Should()
                .BeTrue();
        }

        [TestCase("shortest")]
        [TestCase("fastest")]
        public void Validate_GivenValidRouteType_ShouldNotHaveErrors(string routeType)
        {
            _routeRequest.Type = routeType;

            _validator
                .Validate(_routeRequest)
                .Errors
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Validate_GivenEmptyRouteType_ShouldContainExpectedErrorMessage()
        {
            _routeRequest.Type = null;

            _validator
                .Validate(_routeRequest)
                .Errors
                .Any(p => p.ErrorMessage == "route type should be informed")
                .Should()
                .BeTrue();
        }
    }
}