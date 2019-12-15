using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public void Run(IEnumerable<string> directoriesToWatch)
        {
            var watchers = directoriesToWatch
                .Select(s =>
                {
                    var watcher = new FileSystemWatcher()
                    {
                        Path = s,
                        NotifyFilter = NotifyFilters.CreationTime 
                                        | NotifyFilters.FileName,
                        Filter = "*.done",
                        IncludeSubdirectories = true
                    };

                    watcher.Created += OnChange;
                    watcher.Deleted += OnChange;
                    watcher.Renamed += OnRename;

                    watcher.EnableRaisingEvents = true;
                    return watcher;
                }).ToList();


            while (true) ;
        }

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"System event found: {e.ChangeType.ToString()}. Reporting Now");
            Reporter.Report(e);
        }

        private void OnRename(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"System event found: {e.ChangeType.ToString()}. Reporting Now");
            Reporter.Report(e);
        }
    }
}
