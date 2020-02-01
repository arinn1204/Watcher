using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner.Reporter.RabbitMQReporter
{
    public class RabbitReporter : IReporter
    {
        private readonly IModel _model;
        private readonly string _exchangeName;
        private readonly string _routingPattern;

        public RabbitReporter(
            IModel model,
            string exchangeName,
            string routingPattern)
        {
            _model = model;
            _exchangeName = exchangeName;
            _routingPattern = routingPattern;
        }

        public void Report<T>(T message)
        {
            var properties = _model.CreateBasicProperties();
            var messageType = message.GetType();
            byte[] convertedMessage;
            if (messageType == typeof(string) || messageType.BaseType == typeof(Enum))
            {
                Console.WriteLine($"Publishing: {message}");
                convertedMessage = Encoding.UTF8.GetBytes(message.ToString());
                properties.ContentType = "text/plain; charset=utf-8";
            }
            else
            {
                var stringMessage = JsonConvert.SerializeObject(message);
                convertedMessage = Encoding.UTF8.GetBytes(stringMessage);

                properties.ContentType = messageType.IsValueType
                    ? $"application/c#.{messageType.Name.ToLowerInvariant()}; charset=utf-8"
                    : "application/json; charset=utf-8";

                Console.WriteLine($"Publishing: {stringMessage}");
            }


            _model.BasicPublish(
                _exchangeName,
                _routingPattern,
                properties,
                convertedMessage);
        }

        public Task ReportAsync<T>(T message)
        {
            return Task.Run(() => Report(message));
        }
    }
}
