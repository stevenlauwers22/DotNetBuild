using System;
using DotNetBuild.Runner.Infrastructure.Logging.Loggers;

namespace DotNetBuild.Runner.Infrastructure.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            var runningFromTeamCity = Environment.GetEnvironmentVariable("TEAMCITY_PROJECT_NAME") != null;
            if (runningFromTeamCity)
                return new TeamCityLogger();

            return new StandardLogger();
        }
    }
}