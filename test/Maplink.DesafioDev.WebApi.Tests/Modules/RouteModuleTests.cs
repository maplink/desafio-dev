using System;
using System.Threading.Tasks;
using FluentAssertions;
using Maplink.DesafioDev.Commands;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.WebApi.Modules;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Maplink.DesafioDev.WebApi.Tests.Modules
{
    [TestFixture]
    public class RouteModuleTests
    {
        private Mock<DoRouteCommand> _commandMock;

        private BrowserResponse _response;

        [SetUp]
        public void SetUp()
        {
            _commandMock = new Mock<DoRouteCommand>();

            _commandMock
                .Setup(p => p.Execute(It.IsAny<RouteRequest>()))
                .Returns(Task.FromResult(new RouteResponse(new RouteResponseItem())));
        }

        [Test]
        public void Post_GivenValidRequest_ShouldReturn200Ok()
        {
            var validRequest = GivenValidRouteRequest();

            Post(validRequest);

            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }

        [Test]
        public void Post_GivenIncompleteRequest_ShouldReturn422UnprocessableEntity()
        {
            var validRequest = GivenIncompleteRouteRequest();

            Post(validRequest);

            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.UnprocessableEntity);
        }


        [Test]
        public void Post_GivenInvalidRequest_ShouldReturn500InternalServerError()
        {
            Post("invalid-request");

            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void Post_WhenAnExceptionIsThrow_ShouldReturn500InternalServerError()
        {
            _commandMock
                .Setup(p => p.Execute(It.IsAny<RouteRequest>()))
                .Throws(new Exception());

            var validRequest = GivenValidRouteRequest();

            Post(validRequest);

            _response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.InternalServerError);
        }

        private static string GivenValidRouteRequest()
        {
            return @"{""addresses"":[{""street"":""av paulista"",""number"":""1"",""city"":""sao paulo"",""state"":""sp""},{""street"":""av paulista"",""number"":""1000"",""city"":""sp"",""state"":""sp""}],""type"":""shortest""}";
        }

        private string GivenIncompleteRouteRequest()
        {
            return @"{""addresses"":[{""street"":"""",""number"":""1"",""city"":""sao paulo"",""state"":""sp""},{""street"":"""",""number"":""1000"",""city"":""sp"",""state"":""sp""}],""type"":""shortest""}";
        }

        private void Post(string body)
        {
            var browser = new Browser(with => with.Module(new RouteModule(_commandMock.Object)));

            _response = browser.Post("/routes", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Body(body);
            });
        }
    }
}