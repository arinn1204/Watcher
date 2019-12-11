using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using Watcher.Interfaces;
using Watcher.Runner.Interfaces;
using Watcher.Runner.RabbitReporter.Extensions;

namespace Watcher.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("WATCHER_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();

            var startup = new Startup(config);
            var serviceContainer = BuildServiceContainer(startup);

            BuildWatcher(serviceContainer)
                .Run(args);
        }

        private static IWatcher BuildWatcher(IContainer serviceContainer)
        {
            var watcherBuilder = serviceContainer.Resolve<IWatcherBuilder>();

            return watcherBuilder
                .WithConfiguration(serviceContainer.Resolve<IConfiguration>())
                .AddRabbitReporter()
                .Build();
        }

        private static IContainer BuildServiceContainer(Startup startup)
        {
            var containerBuilder = new ContainerBuilder();
            return startup.ConfigureServices(containerBuilder);
        }
    }
}
