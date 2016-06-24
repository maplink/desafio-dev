using System.Collections.Generic;
using System.Linq;
using Maplink.DesafioDev.Commands;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Commands
{
    [TestFixture]
    public class DoRouteCommandTests
    {
        private Mock<SearchService> _searchServiceMock;
        private Mock<RouteService> _routeServiceMock;

        private DoRouteCommand _command;
        private RouteRequest _routeRequest;

        [SetUp]
        public void SetUp()
        {
            _searchServiceMock = new Mock<SearchService>();
            _routeServiceMock = new Mock<RouteService>();

            _searchServiceMock
                .Setup(p => p.GetLocation(It.IsAny<Address>()))
                .ReturnsAsync(new Location());

            _command = new DoRouteCommand(_searchServiceMock.Object, _routeServiceMock.Object);

            _routeRequest = new RouteRequest
            {
                Addresses = Enumerable.Repeat(new Address(), 3),
                Type = "shortest"
            };
        }

        [Test]
        public async void Execute_GivenRouteRequest_ShouldCallGetLocationExpectedTimes()
        {
            var expected = _routeRequest.Addresses.Count();

            await _command.Execute(_routeRequest);

            _searchServiceMock.Verify(p => p.GetLocation(It.IsAny<Address>()), Times.Exactly(expected));
        }

        [Test]
        public async void Execute_GivenRouteRequest_ShouldCallGetRouteData()
        {
            await _command.Execute(_routeRequest);

            _routeServiceMock.Verify(p => p.GetRouteData(It.IsAny<IEnumerable<Location>>(), "shortest"), Times.Once);
        }
    }
}