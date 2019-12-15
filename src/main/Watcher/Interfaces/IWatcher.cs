using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Watcher.Runner.Interfaces;

namespace Watcher.Interfaces
{
    public interface IWatcher
    {
        IReporter Reporter { get; }
        IConfiguration Configuration { get; }
        Task Run(string[] args);
    }
}
