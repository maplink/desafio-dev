using System;
using FluentAssertions;
using Maplink.DesafioDev.Infrastructure;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure
{
    [TestFixture]
    public class RestHandlerTests
    {
        private RestHandler _restHandler;

        [SetUp]
        public void SetUp()
        {
            _restHandler = new RestHandler();
        }

        [Test]
        public async void Get_GivenUrl_ShouldReturnContent()
        {
            const string url = "http://www.google.com";

            string result = await _restHandler.Get(url);

            result
                .Should()
                .NotBeNullOrWhiteSpace();
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "failed to consume api rest with get action. url 'http://google.invalid'.")]
        public async void Get_GivenInvalidUrl_ShouldThrowExpectedException()
        {
            const string url = "http://google.invalid";

            await _restHandler.Get(url);
        }
    }
}