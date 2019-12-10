using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using NUnit.Framework;
using Watcher.Runner;
using Watcher.Runner.Builder;

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
    }
}
