using System;
using FluentAssertions;
using FluentValidation.Results;
using Maplink.DesafioDev.Infrastructure.Exceptions;
using Maplink.DesafioDev.Infrastructure.Extensions;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Extensions
{
    [TestFixture]
    public class ValidationResultExtensionsTests
    {
        [Test]
        public void ThrowOnFailure_GivenValidValidationResult_ShouldNotThrowException()
        {
            var validationResult = new ValidationResult();

            Action action = () => validationResult.ThrowOnFailure();

            action.ShouldNotThrow();
        }

        [Test]
        public void ThrowOnFailure_GivenValidationResultWithErrors_ShouldThrowRequestValidationException()
        {
            var validationFailures = new[] {new ValidationFailure("property 1", "error 1")};
            var validationResult = new ValidationResult(validationFailures);

            Action action = () => validationResult.ThrowOnFailure();

            action.ShouldThrow<RequestValidationException>();
        }
    }
}