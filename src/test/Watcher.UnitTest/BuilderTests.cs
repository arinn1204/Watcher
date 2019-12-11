using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Watcher.Runner;
using Watcher.Runner.Builder;
using Watcher.Runner.Interfaces;
using Watcher.Runner.RabbitReporter.Configuration;
using Watcher.Runner.Reporter.RabbitReporter;

namespace Watcher.UnitTest
{
    [TestFixture]
    public class BuilderTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void ShouldReturnWatcher()
        {
            var builder = _fixture.Create<WatcherBuilder>();
            var watcher = builder.Build();

            watcher.Should().BeOfType<SystemWatcher>();
        }

        [Test]
        public void ShouldAddProvidedReporter()
        {
            var builder = _fixture.Create<WatcherBuilder>();
            var reporter = _fixture.Freeze<IReporter>();

            var watcher = builder
                .WithReporter(reporter)
                .Build();

            watcher.Reporter
                .Should()
                .Be(reporter);
        }

        [Test]
        public void ShouldAddIConfiguration()
        {
            var builder = _fixture.Create<WatcherBuilder>();
            var reporter = _fixture.Freeze<IConfiguration>();

            var watcher = builder
                .WithConfiguration(reporter)
                .Build();

            watcher.Configuration
                .Should()
                .Be(reporter);

        }
    }
}
