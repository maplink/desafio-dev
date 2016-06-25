using Maplink.DesafioDev.Infrastructure;
using Maplink.DesafioDev.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Services
{
    [TestFixture]
    public class MaplinkServiceTests
    {
        private MaplinkService _service;

        private Mock<MaplinkSignUrl> _maplinkSignUrlMock;
        private Mock<RestHandler> _restHandlerMock;

        private const string Token = "token";

        [SetUp]
        public void SetUp()
        {
            _maplinkSignUrlMock = new Mock<MaplinkSignUrl>();
            _restHandlerMock = new Mock<RestHandler>();

            _service = new MaplinkService(Token, _maplinkSignUrlMock.Object, _restHandlerMock.Object);
        }

        [Test]
        public async void GetContent_GivenRequestUri_ShouldCallGet()
        {
            await _service.GetContent("requestUri");

            _restHandlerMock.Verify(p => p.Get("requestUri"), Times.Once);
        }

        [Test]
        public void Sign_GivenUrl_ShouldCallSignOn()
        {
            _service.Sign("url");

            _maplinkSignUrlMock.Verify(p => p.Sign("url", Token), Times.Once);
        }
    }
}