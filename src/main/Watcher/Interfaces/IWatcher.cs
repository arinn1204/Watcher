using System;
using System.Collections.Generic;
using System.Text;
using Watcher.Runner.Interfaces;

namespace Watcher.Interfaces
{
    public interface IWatcher
    {
        IReporter Reporter { get; }
        void Run(string[] args);
    }
}
