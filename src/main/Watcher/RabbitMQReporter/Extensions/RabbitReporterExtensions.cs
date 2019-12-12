using Microsoft.Extensions.Configuration;
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
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder, IConfiguration configuration)
        {
            var factory = new ConnectionFactory();

            var connection = factory.CreateConnection();
            var reporter = new RabbitReporter(connection);
            return builder.WithReporter(reporter);
        }
    }
}
