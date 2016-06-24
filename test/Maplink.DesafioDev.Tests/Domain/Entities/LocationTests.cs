using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Domain.Entities
{
    [TestFixture]
    public class LocationTests
    {
        private Location _location;

        [SetUp]
        public void SetUp()
        {
            _location = new Location
            {
                Latitude = -23.53221,
                Longitude = -46.79006
            };
        }

        [Test]
        public void ToWaypoint_GivenIndex_ShouldReturnExpectedValue()
        {
            const int index = 0;

            _location
                .ToWaypoint(index)
                .Should()
                .Be("waypoint.0.latlng=-23.53221,-46.79006");
        }
    }
}