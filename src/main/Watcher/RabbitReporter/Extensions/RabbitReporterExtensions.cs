using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Interfaces;

namespace Watcher.Runner.RabbitReporter.Extensions
{
    public static class RabbitReporterExtensions
    {
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder)
        {
            return builder;
        }
    }
}
