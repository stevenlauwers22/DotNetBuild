﻿using System;
using DotNetBuild.Core.Facilities.Logging;

namespace DotNetBuild.Runner.Infrastructure.Facilities.Logging
{
    public class Logger
        : ILogger
    {
        private readonly Infrastructure.Logging.ILogger _logger;

        public Logger(Infrastructure.Logging.ILogger logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message)
        {
            _logger.Write(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.WriteError(message, exception);
        }
    }
}