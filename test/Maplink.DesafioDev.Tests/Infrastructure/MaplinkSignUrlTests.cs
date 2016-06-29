using FluentAssertions;
using Maplink.DesafioDev.Infrastructure;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure
{
    [TestFixture]
    public class MaplinkSignUrlTests
    {
        private MaplinkSignUrl _maplinkSignUrl;

        [SetUp]
        public void SetUp()
        {
            _maplinkSignUrl = new MaplinkSignUrl();
        }

        [Test]
        public void Sign_GivenUrlAndToken_ShouldReturnUrlWithSignature()
        {
            const string url = "http://www.google.com/?parameter1=value";
            const string validToken = "z0vmywzpbCSLdJYl5mUk5m2jNGytNGt6NJu6NGU=";

            var signedUrl = _maplinkSignUrl.Sign(url, validToken);

            signedUrl
                .Contains("&signature=")
                .Should()
                .BeTrue();

        }
    }
}