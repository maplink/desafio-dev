using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;
using Maplink.DesafioDev.Infrastructure.Exceptions;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Exceptions
{
    [TestFixture]
    public class RequestValidationExceptionTests
    {
        [Test]
        public void GetErrors_WhenCreatingRequestValidationWithErrorMessage_ShouldReturnExpectedValues()
        {
            new RequestValidationException("error message")
                .GetErrors()
                .ShouldBeEquivalentTo(new[] {"error message"});
        }

        [Test]
        public void GetErrors_WhenCreatingRequestValidationWithMultipleErrorMessages_ShouldReturnExpectedValues()
        {
            new RequestValidationException(new[] {"error 1", "error 2"})
                .GetErrors()
                .ShouldBeEquivalentTo(new[] {"error 1", "error 2"});
        }

        [Test]
        public void GetErrors_WhenCreatingRequestValidationWithValidationFailures_ShouldReturnExpectedValues()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("property 1", "error 1"),
                new ValidationFailure("property 2", "error 2")
            };

            new RequestValidationException(validationFailures)
                .GetErrors()
                .ShouldBeEquivalentTo(new[] { "error 1", "error 2" });
        }
    }
}