using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task Run(string[] args)
        {
            await Reporter.ReportAsync("Hello, World!");
        }
    }
}
