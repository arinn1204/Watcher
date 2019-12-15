using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Watcher.Runner.Interfaces
{
    public interface IReporter
    {
        void Report<T>(T message);
        Task ReportAsync<T>(T message);
    }
}
