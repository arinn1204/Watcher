using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Watcher.Runner.Reporter.RabbitMQReporter;

namespace Watcher.UnitTest
{
    [TestFixture]
    public class RabbitReporterTests
    {
        private Fixture _fixture;

        private enum TestEnum { SomeValue }

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        [TestCase("Int16")]
        [TestCase("Int32")]
        [TestCase("Int64")]
        [TestCase("Single")]
        [TestCase("Double")]
        [TestCase("Char")]
        [TestCase("Boolean")]
        public void ShouldSetContentTypeToTypeOfParameterIfValueType(string type)
        {
            var model = _fixture.Freeze<Mock<IModel>>();
            var reporter = _fixture.Create<RabbitReporter>();
            var messageType = Type.GetType($"System.{type}");
            var message = GetType()
                .GetMethod(
                    "DefaultOfType",
                    BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(messageType)
                .Invoke(this, null);

            var messageProperties = CreateMessageProperties(model);

            reporter.Report(message);

            var (properties, _) = messageProperties();

            properties
                .ContentType
                .Should()
                .Be($"application/c#.{type.ToLowerInvariant()}");
        }

        [Test]
        public void ShouldSetContentTypeToPlainTextIfMessageIsString()
        {
            var model = _fixture.Freeze<Mock<IModel>>();
            var reporter = _fixture.Create<RabbitReporter>();
            var message = "hunter2";
            var messageProperties = CreateMessageProperties(model);

            reporter.Report(message);

            var (properties, _) = messageProperties();

            properties
                .ContentType
                .Should()
                .Be("text/plain");
        }

        [Test]
        public void ShouldSetContentTypeToPlainTextIfMessageIsEnum()
        {
            var model = _fixture.Freeze<Mock<IModel>>();
            var reporter = _fixture.Create<RabbitReporter>();
            var message = TestEnum.SomeValue;
            var messageProperties = CreateMessageProperties(model);

            reporter.Report(message);

            var (properties, _) = messageProperties();

            properties
                .ContentType
                .Should()
                .Be("text/plain");
        }

        [Test]
        public void ShouldSetContentTypeToJsonIfMessageIsAnObject()
        {
            var model = _fixture.Freeze<Mock<IModel>>();
            var reporter = _fixture.Create<RabbitReporter>();
            var message = new { SomeProp = true };
            var messageProperties = CreateMessageProperties(model);

            reporter.Report(message);

            var (properties, _) = messageProperties();

            properties
                .ContentType
                .Should()
                .Be("application/json");
        }

        [Test]
        public void ShouldCallPublishWithEncodedVersionMessage()
        {
            var model = _fixture.Freeze<Mock<IModel>>();
            var reporter = _fixture.Create<RabbitReporter>();
            var message = new List<int>() { 1, 2, 3 };
            var messageProperties = CreateMessageProperties(model);

            reporter.Report(message);

            var (_, body) = messageProperties();

            var json = Encoding.UTF8.GetString(body);
            JsonConvert.DeserializeObject<List<int>>(json)
                .Should()
                .BeEquivalentTo(message);
        }

        private Func<(IBasicProperties,byte[])> CreateMessageProperties(Mock<IModel> model)
        {
            model.Setup(s => s.CreateBasicProperties())
                .Returns(new BasicProperties());

            var messageProperties = default(IBasicProperties);
            var messageBody = Array.Empty<byte>();

            model.Setup(
                s => s.BasicPublish(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<IBasicProperties>(),
                    It.IsAny<byte[]>()))
                .Callback(
                (string exchange,
                    string routingKey,
                    bool mandatory,
                    IBasicProperties basicProperties,
                    byte[] body) =>
                {
                    messageProperties = basicProperties;
                    messageBody = body;
                });
            return () => (messageProperties, messageBody);
        }

        private T DefaultOfType<T>()
        {
            return default;
        }
    }
}
