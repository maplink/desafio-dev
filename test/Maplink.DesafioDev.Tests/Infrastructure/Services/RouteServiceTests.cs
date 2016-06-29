using System.Collections.Generic;
using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Services
{
    [TestFixture]
    public class RouteServiceTests
    {
        private Mock<MaplinkService> _maplinkServiceMock;
        private const string ApiRouteUrl = "http://api.com";
        private const string ApplicationCode = "application-code";
        private const string RouteType = "shortest";

        private RouteService _routeService;

        [SetUp]
        public void SetUp()
        {
            _maplinkServiceMock = new Mock<MaplinkService>();

            const string json = @"{""routes"":[{""summary"":{""distance"":1,""duration"":2}}]}";
            dynamic routeResponse = JsonConvert.DeserializeObject(json);

            ReturnsExtensions
                .ReturnsAsync(_maplinkServiceMock
                .Setup(p => p.GetContent(It.IsAny<string>())), routeResponse);

            _routeService = new StubRouteService(ApiRouteUrl, ApplicationCode, _maplinkServiceMock.Object);
        }

        [Test]
        public async void GetRouteData_GivenLocations_ShouldCallGetContentWithExpectedRequestUri()
        {
            const string expected = "http://api.com/?waypoint.0.latlng=1,2&waypoint.1.latlng=3,4&travel.mode=shortest&applicationCode=application-code&signature=123";

            var locations = GetLocations();

            await _routeService.GetRouteData(locations, RouteType);

            _maplinkServiceMock.Verify(p => p.GetContent(expected));
        }

        [Test]
        public async void GetRouteData_GivenLocations_ShouldReturnValidRouteResponse()
        {
            var locations = GetLocations();

            var routeResponse = await _routeService.GetRouteData(locations, RouteType);

            routeResponse
                .Success
                .Should()
                .BeTrue();
        }

        [Test]
        public async void GetRouteData_GivenLocations_ShouldCallSign()
        {
            var locations = GetLocations();

            var routeService = new RouteService(ApiRouteUrl, ApplicationCode, _maplinkServiceMock.Object);

            await routeService.GetRouteData(locations, RouteType);

            _maplinkServiceMock.Verify(p => p.GetContent(It.IsAny<string>()), Times.Once);
        }

        private static IEnumerable<Location> GetLocations()
        {
            return new[]
            {
                new Location {Latitude = 1, Longitude = 2},
                new Location {Latitude = 3, Longitude = 4}
            };
        }

        class StubRouteService : RouteService
        {
            public StubRouteService(string apiRouteUrl, string applicationCode, MaplinkService maplinkService)
                : base(apiRouteUrl, applicationCode, maplinkService)
            {
            }

            protected override string Sign(string uri)
            {
                return $"{uri}&signature=123";
            }
        }
    }
}