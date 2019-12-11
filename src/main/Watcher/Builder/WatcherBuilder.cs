using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Interfaces;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner.Builder
{
    public class WatcherBuilder : IWatcherBuilder
    {
        private IReporter _reporter;
        private IConfiguration _configuration;


        public IWatcher Build()
        {
            return new SystemWatcher(_reporter);
        }

        public IWatcherBuilder WithReporter(IReporter reporter)
        {
            _reporter = reporter;
            return this;
        }
    }
}
