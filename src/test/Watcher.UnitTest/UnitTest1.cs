using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;

namespace Watcher.UnitTest
{
    public class Tests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}