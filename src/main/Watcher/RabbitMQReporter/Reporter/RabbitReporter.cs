using RabbitMQ.Client;
using System;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner.Reporter.RabbitMQReporter
{
    public class RabbitReporter : IReporter
    {
        public RabbitReporter(IConnection connection)
        {
            Connection = connection;
        }

        public IConnection Connection { get; }

        public void Report(string textToReport)
        {
            throw new NotImplementedException();
        }
    }
}
