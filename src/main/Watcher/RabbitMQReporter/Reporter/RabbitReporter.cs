using System;
using Watcher.Runner.Interfaces;
using Watcher.Runner.RabbitMQReporter.Configuration;

namespace Watcher.Runner.Reporter.RabbitMQReporter
{
    public class RabbitReporter : IReporter
    {
        public RabbitReporter(RabbitReporterOptions configuration)
        {
            Configuration = configuration;
        }

        public RabbitReporterOptions Configuration { get; }

        public void Report(string textToReport)
        {
            throw new NotImplementedException();
        }
    }
}
