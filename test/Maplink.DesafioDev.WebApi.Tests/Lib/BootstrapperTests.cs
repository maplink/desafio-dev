using System;
using FluentAssertions;
using FluentValidation;
using LightInject;
using Maplink.DesafioDev.Commands;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure;
using Maplink.DesafioDev.Infrastructure.Services;
using Maplink.DesafioDev.WebApi.Lib;
using NUnit.Framework;

namespace Maplink.DesafioDev.WebApi.Tests.Lib
{
    [TestFixture]
    public class BootstrapperTests
    {
        private IServiceContainer _serviceContainer;

        [TestFixtureSetUp]
        public void SetUp()
        {
            using (var bootstrapper = new Bootstrapper())
            {
                bootstrapper.Initialise();
                _serviceContainer = bootstrapper.Container;
            }
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _serviceContainer?.Dispose();
        }

        [TestCaseSource(nameof(Types))]
        public void GetInstance_GivenReference_ShouldCreateAnInstanceOf(Type type)
        {
            _serviceContainer
                .GetInstance(type)
                .Should()
                .NotBeNull();
        }

        static readonly Type[] Types =
        {
            typeof(MaplinkService),
            typeof(SearchService),
            typeof(RouteService),
            typeof(DoRouteCommand),
            typeof(RestHandler),
            typeof(MaplinkSignUrl),
            typeof(IValidator<RouteRequest>)
        };
    }
}