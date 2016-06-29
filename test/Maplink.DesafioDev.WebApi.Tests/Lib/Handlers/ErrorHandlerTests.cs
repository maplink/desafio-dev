using System;
using System.IO;
using FluentAssertions;
using Maplink.DesafioDev.Infrastructure.Exceptions;
using Maplink.DesafioDev.WebApi.Lib.Handlers;
using Nancy;
using NUnit.Framework;

namespace Maplink.DesafioDev.WebApi.Tests.Lib.Handlers
{
    [TestFixture]
    public class ErrorHandlerTests
    {
        private NancyContext _context;
        private ErrorHandler _errorHandler;

        [SetUp]
        public void SetUp()
        {
            _context = new NancyContext();
            _errorHandler = new ErrorHandler();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public void ErrorHandler_GivenGenericError_ShoulReturnHttpStatus500InternalServerError()
        {
            _errorHandler
                .OnError(_context, new Exception("Erro"))
                .StatusCode
                .Should()
                .Be(HttpStatusCode.InternalServerError);
        }

        [Test]
        public void ErrorHandler_GivenGenericError_ShoulReturnMessage()
        {
            using (var memoryStream = new MemoryStream())
            {
                _errorHandler
                    .OnError(_context, new Exception("error 1"))
                    .Contents(memoryStream);

                memoryStream.Position = 0;

                var streamReader = new StreamReader(memoryStream);

                var json = streamReader.ReadToEnd();

                json
                    .Should()
                    .Contain("\"errors\":[\"error 1\"]");
            }
        }

        [Test]
        public void ErrorHandler_GivenRequestValidationException_ShoulReturnMessage()
        {
            using (var memoryStream = new MemoryStream())
            {
                _errorHandler
                    .OnError(_context, new RequestValidationException(new[] { "error 1", "error 2" }))
                    .Contents(memoryStream);

                memoryStream.Position = 0;

                var streamReader = new StreamReader(memoryStream);
                var json = streamReader.ReadToEnd();

                json
                    .Should()
                    .Contain("\"errors\":[\"error 1\",\"error 2\"]");
            }
        }

        [Test]
        public void ErrorHandler_GivenRequestValidationException_ShoulReturn422UnprocessableEntity()
        {
            _errorHandler
                .OnError(_context, new RequestValidationException(new[] { "error 1", "error 2" }))
                .StatusCode
                .Should()
                .Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}