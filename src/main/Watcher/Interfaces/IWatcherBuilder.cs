using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Runner.Interfaces;

namespace Watcher.Interfaces
{
    public interface IWatcherBuilder
    {
        IWatcher Build();
        IWatcherBuilder WithReporter(IReporter reporter);
    }
}
