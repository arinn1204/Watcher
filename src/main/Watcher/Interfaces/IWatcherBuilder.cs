using System;
using System.Collections.Generic;
using System.Text;

namespace Watcher.Interfaces
{
    public interface IWatcherBuilder
    {
        IWatcher Build();
    }
}
