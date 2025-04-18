using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Core", "Logs");

        static Logger()
        {
            EnsureLogDirectoryExists();

            // Initialize log4net configuration
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        private static void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void Info(string message) => log.Info(message);
        public static void Debug(string message) => log.Debug(message);
        public static void Warn(string message) => log.Warn(message);
        public static void Error(string message, Exception ex = null) => log.Error(message, ex);
    }
}
