using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Watcher
{
    class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        internal IContainer ConfigureServices(ContainerBuilder container)
        {
            container.RegisterInstance(_config);
            return container.Build();
        }
    }
}
