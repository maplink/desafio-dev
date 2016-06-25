using FluentAssertions;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Services;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Services
{
    [TestFixture]
    public class SearchServiceTests
    {
        private Mock<MaplinkService> _maplinkServiceMock;
        private const string ApiSearchUrl = "http://api.com";
        private const string ApplicationCode = "application-code";

        private SearchService _searchService;
        private Address _address;

        [SetUp]
        public void SetUp()
        {
            _maplinkServiceMock = new Mock<MaplinkService>();

            const string json = @"{""results"":[{""location"":{""lat"":1,""lng"":2}}]}";
            dynamic searchResponse = JsonConvert.DeserializeObject(json);

            ReturnsExtensions
                .ReturnsAsync(_maplinkServiceMock
                .Setup(p => p.GetContent(It.IsAny<string>())), searchResponse);

            _searchService = new StubSearchService(ApiSearchUrl, ApplicationCode, _maplinkServiceMock.Object);

            _address = new Address
            {
                State = "state",
                Street = "street",
                Number = "number",
                City = "city"
            };
        }

        [Test]
        public async void GetRouteData_GivenAddress_ShouldCallGetContentWithExpectedRequestUri()
        {
            const string expected = "http://api.com/?q=street, number, city, state&applicationCode=application-code&signature=123";

            await _searchService.GetLocation(_address);

            _maplinkServiceMock.Verify(p => p.GetContent(expected));
        }

        [Test]
        public async void GetLocation_GivenAddress__ShouldReturnLocation()
        {
            var locations = await _searchService.GetLocation(_address);

            locations
                .Should()
                .NotBeNull();
        }

        [Test]
        public async void GetLocation_GivenAddress_ShouldCallSign()
        {
            var routeService = new SearchService(ApiSearchUrl, ApplicationCode, _maplinkServiceMock.Object);

            await routeService.GetLocation(_address);

            _maplinkServiceMock.Verify(p => p.GetContent(It.IsAny<string>()), Times.Once);
        }

        class StubSearchService : SearchService
        {
            public StubSearchService(string apiSearchUrl, string applicationCode, MaplinkService maplinkService)
                : base(apiSearchUrl, applicationCode, maplinkService)
            {
            }

            protected override string Sign(string uri)
            {
                return $"{uri}&signature=123";
            }
        }
    }
}