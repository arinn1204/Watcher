using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Interfaces;
using Watcher.Runner.RabbitMQReporter.Configuration;
using Watcher.Runner.Reporter.RabbitMQReporter;

namespace Watcher.Runner.RabbitMQReporter.Extensions
{
    public static class RabbitReporterExtensions
    {
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder, RabbitReporterOptions options)
        {
            return builder.WithReporter(new RabbitReporter(options));
        }
    }
}
