using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher.Runner.Interfaces
{
    public interface IReporter
    {
        void Report(object message);
    }
}
