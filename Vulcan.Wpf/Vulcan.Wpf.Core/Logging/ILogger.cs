using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    public interface ILogger
    {
        void Log(string message, Type callerType = null, LogCategory category = LogCategory.Info, LogPriority priority = LogPriority.Low);
        void Log(Exception exception);
    }
}
