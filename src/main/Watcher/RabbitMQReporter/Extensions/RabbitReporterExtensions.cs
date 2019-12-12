using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Interfaces;
using Watcher.Runner.Reporter.RabbitMQReporter;

namespace Watcher.Runner.RabbitMQReporter.Extensions
{
    public static class RabbitReporterExtensions
    {
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder, IConnection connection)
        {
            return builder.WithReporter(new RabbitReporter(connection));
        }
    }
}
