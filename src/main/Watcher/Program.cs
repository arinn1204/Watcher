﻿using Autofac;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using Watcher.Interfaces;
using Watcher.Runner.RabbitMQReporter.Extensions;

namespace Watcher.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Building configuration.");
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("WATCHER_ENVIRONMENT")}.json", true)
                .AddEnvironmentVariables()
                .Build();

            var startup = new Startup(config);
            var serviceContainer = BuildServiceContainer(startup);

            Console.WriteLine("Beginning run.");
            BuildWatcher(serviceContainer)
                .Run(args);
        }

        private static IWatcher BuildWatcher(IContainer container)
        {
            var watcherBuilder = container.Resolve<IWatcherBuilder>();
            var config = container.Resolve<IConfiguration>();

            Console.WriteLine("Building the builder.");
            return watcherBuilder
                .WithConfiguration(config)
                .AddRabbitReporter(config, "RabbitMQ")
                .Build();
        }

        private static IContainer BuildServiceContainer(Startup startup)
        {
            var containerBuilder = new ContainerBuilder();
            return startup.ConfigureServices(containerBuilder);
        }
    }
}