using System;
using DotNetBuild.Core.Facilities.Logging;

namespace DotNetBuild.Runner.Facilities.Logging
{
    public class Logger
        : ILogger
    {
        private readonly Infrastructure.Logging.ILogger _logger;

        public Logger(Infrastructure.Logging.ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            _logger = logger;
        }

        public void LogInfo(String message)
        {
            _logger.Write(message);
        }

        public void LogError(String message, Exception exception)
        {
            _logger.WriteError(message, exception);
        }
    }
}