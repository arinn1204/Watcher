using Autofac;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Watcher.Interfaces;
using Watcher.Runner;

namespace Watcher.UnitTest
{
    public class StartupTests
    {
        private Fixture _fixture;
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            var startup = _fixture.Create<Startup>();
            var containerBuilder = new ContainerBuilder();
            _container = startup.ConfigureServices(containerBuilder);

        }

        [Test]
        public void ShouldRegisterConfiguration()
        {
            _container.IsRegistered<IConfiguration>()
                .Should()
                .BeTrue();
        }

        [Test]
        public void ShouldRegisterWatcherBuilder()
        {
            _container.IsRegistered<IWatcherBuilder>()
                .Should()
                .BeTrue();
        }
    }
}