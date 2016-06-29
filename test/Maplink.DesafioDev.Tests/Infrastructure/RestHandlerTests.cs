using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Maplink.DesafioDev.Infrastructure;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Maplink.DesafioDev.Tests.Infrastructure
{
    [TestFixture]
    public class RestHandlerTests
    {
        [Test]
        public async void Get_GivenValidUrlAndResponse_ShouldNotBeNull()
        {
            const string url = "valid-url";

            var restHandler = new StubRestHandler();

            object result = await restHandler.Get(url);

            result
                .Should()
                .NotBeNull();
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "failed to consume api rest with get action. url 'http://google.invalid'.")]
        public async void Get_GivenInvalidUrl_ShouldThrowExpectedException()
        {
            const string url = "http://google.invalid";

            await new RestHandler().Get(url);
        }

        class StubRestHandler : RestHandler
        {
            protected override async Task<IRestResponse> GetContent(string url, IRestRequest request)
            {
                var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

                taskCompletionSource.SetResult(new RestResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = @"{""parameter"":""value""}"
                });

                return await taskCompletionSource.Task;
            }
        }
    }
}