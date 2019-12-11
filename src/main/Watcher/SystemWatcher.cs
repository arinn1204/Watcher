using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Interfaces;
using Watcher.Runner.Interfaces;

namespace Watcher.Runner
{
    public class SystemWatcher : IWatcher
    {
        public SystemWatcher(IReporter reporter)
        {
            Reporter = reporter;
        }

        public IReporter Reporter { get; }

        public void Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
