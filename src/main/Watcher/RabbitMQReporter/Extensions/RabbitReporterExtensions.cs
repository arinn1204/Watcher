using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Watcher.Interfaces;
using Watcher.Runner.Reporter.RabbitMQReporter;

namespace Watcher.Runner.RabbitMQReporter.Extensions
{
    public static class RabbitReporterExtensions
    {
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder, IConfiguration configuration)
        {
            var rabbitConfiguration = configuration.GetSection("RabbitMQ");
            var factory = new ConnectionFactory()
            {
                UserName = rabbitConfiguration.GetSection("Username").Value,
                Password = rabbitConfiguration.GetSection("Password").Value,
                HostName = rabbitConfiguration.GetSection("HostName").Value,
                VirtualHost = rabbitConfiguration.GetSection("VirtualHost").Value,
                Port = rabbitConfiguration.GetValue<int>("Port")
            };

            var connection = factory.CreateConnection();
            var reporter = new RabbitReporter(connection);
            return builder.WithReporter(reporter);
        }
    }
}
