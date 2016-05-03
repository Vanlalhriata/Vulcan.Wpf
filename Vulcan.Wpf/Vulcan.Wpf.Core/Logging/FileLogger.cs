using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    // TODO: Log conditionally based on LogCategory

    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        private const string DATETIME_FORMAT = "[hh:mm:ss tt][dd-MMM-yyyy]";
        private static string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private string currentLogPath;

        public FileLogger()
        {
            createLogFile();
        }

        private void createLogFile()
        {
            var now = DateTime.Now;
            string currentLogFolder = Path.Combine(logFolder, now.ToString("yyyy"), now.ToString("MMMM"));

            if (!Directory.Exists(currentLogFolder))
                Directory.CreateDirectory(currentLogFolder);

            currentLogPath = Path.Combine(currentLogFolder, now.ToString("yyyy-MM-dd") + ".txt");
        }

        public void Log(string message, Type callerType = null, LogCategory category = LogCategory.Info, LogPriority priority = LogPriority.Low)
        {
            var prefix
                = DateTime.Now.ToString(DATETIME_FORMAT)
                + $"[{category}][Priority:{priority}]";

            if (null != callerType)
                prefix += $"{callerType.FullName}: ";
                
            logToFile(prefix + message + Environment.NewLine);
        }

        public void Log(Exception exception)
        {
            var divider = "---" + Environment.NewLine;
            
            var exceptionMessage
                = divider
                + DateTime.Now.ToString(DATETIME_FORMAT) + $"[{LogCategory.Exception}]{Environment.NewLine}"
                + $"{exception.GetType()}: {exception.Message}{Environment.NewLine}"
                + $"Source: {exception.Source}{Environment.NewLine}"
                + $"Stack trace: {Environment.NewLine}{exception.StackTrace}{Environment.NewLine}"
                + divider;

            logToFile(exceptionMessage);
        }

        private void logToFile(string finalMessage)
        {
            try
            {
                File.AppendAllText(currentLogPath, finalMessage);
            }
            catch
            {
                throw;
            }
        }
    }
}
