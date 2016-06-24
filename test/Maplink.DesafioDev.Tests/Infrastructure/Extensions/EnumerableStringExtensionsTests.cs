using FluentAssertions;
using Maplink.DesafioDev.Infrastructure.Extensions;
using NUnit.Framework;

namespace Maplink.DesafioDev.Tests.Infrastructure.Extensions
{
    [TestFixture]
    public class EnumerableStringExtensionsTests
    {
        [Test]
        public void ToErrorMessage_GivenListOfErrors_ShouldReturnExpectedValue()
        {
            var errors = new[] {"error 1", "error 2", "error 3"};

            errors
                .ToErrorMessage()
                .Should()
                .Be("error 1\r\nerror 2\r\nerror 3\r\n");
        }
    }
}