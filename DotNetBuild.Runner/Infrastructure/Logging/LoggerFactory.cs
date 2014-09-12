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

            var runningFromAppVeyor = Environment.GetEnvironmentVariable("APPVEYOR_API_URL") != null;
            if (runningFromAppVeyor)
                return new AppVeyorLogger();

            return new StandardLogger();
        }
    }
}