using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Watcher.Interfaces;
using Watcher.Runner.Reporter.RabbitMQReporter;

namespace Watcher.Runner.RabbitMQReporter.Extensions
{
    public static class RabbitReporterExtensions
    {
        public static IWatcherBuilder AddRabbitReporter(this IWatcherBuilder builder, IConfiguration configuration, string baseSection)
        {
            var rabbitConfiguration = configuration.GetSection(baseSection);
            var factory = new ConnectionFactory()
            {
                UserName = rabbitConfiguration.GetSection("Username").Value,
                Password = rabbitConfiguration.GetSection("Password").Value,
                HostName = rabbitConfiguration.GetSection("Hostname").Value,
                VirtualHost = rabbitConfiguration.GetSection("VirtualHost").Value,
                Port = rabbitConfiguration.GetValue<int>("Port")
            };

            var exchangeConfiguration = configuration.GetSection($"{baseSection}:Exchange");
            var queueConfiguration = configuration.GetSection($"{baseSection}:Queue");

            var connection = factory.CreateConnection();
            var model = connection.CreateModel();

            model.ExchangeDeclare(
                exchangeConfiguration.GetSection("Name").Value,
                exchangeConfiguration.GetSection("Type").Value,
                exchangeConfiguration.GetValue<bool>("Durable"),
                exchangeConfiguration.GetValue<bool>("Autodelete"));

            model.QueueDeclare(
                queueConfiguration.GetSection("Name").Value,
                queueConfiguration.GetValue<bool>("Durable"),
                queueConfiguration.GetValue<bool>("Exclusive"),
                queueConfiguration.GetValue<bool>("Autodelete"));

            model.QueueBind(
                queueConfiguration.GetSection("Name").Value,
                exchangeConfiguration.GetSection("Name").Value,
                rabbitConfiguration.GetSection("RoutingPattern").Value);

            var reporter = new RabbitReporter(model);
            return builder.WithReporter(reporter);
        }
    }
}
