using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;
using Watcher.Interfaces;
using Watcher.Runner.Builder;

namespace Watcher.Runner
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IContainer ConfigureServices(ContainerBuilder container)
        {
            container.RegisterInstance(_config);
            container.RegisterType<WatcherBuilder>().As<IWatcherBuilder>();
            return container.Build();
        }
    }
}
