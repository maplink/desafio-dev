using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Entities
{
    [TestFixture]
    public class RouteRequestTests
    {
        private RouteRequest _routeRequest;

        [SetUp]
        public void SetUp()
        {
            _routeRequest = new RouteRequest();
        }

        [TestCase("shortest")]
        [TestCase("fastest")]
        [TestCase("FASTEST")]
        public void IsValidType_GivenValidRouteType_ShouldBeTrue(string routeType)
        {
            _routeRequest.Type = routeType;

            _routeRequest
                .IsValidType()
                .Should()
                .BeTrue();
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("longest")]
        [TestCase("slowest")]
        public void IsValidType_GivenInvalidRouteType_ShouldBeFalse(string routeType)
        {
            _routeRequest.Type = routeType;

            _routeRequest
                .IsValidType()
                .Should()
                .BeFalse();
        }
    }
}