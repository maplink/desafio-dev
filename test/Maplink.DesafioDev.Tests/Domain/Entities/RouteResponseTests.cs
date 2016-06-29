using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Entities
{
    [TestFixture]
    public class RouteResponseTests
    {
        [Test]
        public void Errors_WhenCreatingRouteResponseWithRouteResponseItem_ShouldBeEmpty()
        {
            new RouteResponse(new RouteResponseItem())
                .Errors
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Success_WhenCreatingRouteResponseWithRouteResponseItem_ShouldBeTrue()
        {
            new RouteResponse(new RouteResponseItem())
                .Success
                .Should()
                .BeTrue();
        }

        [Test]
        public void Success_WhenCreatingRouteResponseWithErrors_ShouldBeFalse()
        {
            var errors = new[] {"error 1"};

            new RouteResponse(errors)
                .Success
                .Should()
                .BeFalse();
        }

        [Test]
        public void Data_WhenCreatingRouteResponseWithErrors_ShouldBeEmpty()
        {
            var errors = new[] { "error 1" };

            new RouteResponse(errors)
                .Data
                .Should()
                .BeEmpty();
        }
    }
}