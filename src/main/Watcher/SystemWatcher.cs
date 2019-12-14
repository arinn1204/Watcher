using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Watcher.Interfaces;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner
{
    public class SystemWatcher : IWatcher
    {
        public SystemWatcher(IReporter reporter, IConfiguration configuration)
        {
            Reporter = reporter;
            Configuration = configuration;
        }

        public IReporter Reporter { get; }

        public IConfiguration Configuration { get; }

        public void Run(string[] args)
        {
            Reporter.Report("Hello, World!");
        }
    }
}
