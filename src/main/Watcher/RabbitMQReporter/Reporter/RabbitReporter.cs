using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner.Reporter.RabbitMQReporter
{
    public class RabbitReporter : IReporter
    {
        private readonly IModel _model;

        public RabbitReporter(IModel model)
        {
            _model = model;
        }


        public void Report<T>(T message)
        {
            var stringMessage = JsonConvert.SerializeObject(message);
            var convertedMessage = Encoding.UTF8.GetBytes(stringMessage);
            throw new NotImplementedException();
        }
    }
}
