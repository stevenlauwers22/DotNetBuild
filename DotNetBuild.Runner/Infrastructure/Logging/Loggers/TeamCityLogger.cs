using System;

namespace DotNetBuild.Runner.Infrastructure.Logging.Loggers
{
    // http://confluence.jetbrains.com/display/TCD7/Build+Script+Interaction+with+TeamCity#BuildScriptInteractionwithTeamCity-CommonProperties
    public class TeamCityLogger : ILogger
    {
        public void Write(String message)
        {
            Console.WriteLine("##teamcity[message text='{0}']", Escape(message));
        }

        public void WriteBlockStart(String message)
        {
            Console.WriteLine("##teamcity[progressStart '{0}']", Escape(message));
            Console.WriteLine("##teamcity[blockOpened name='{0}']", Escape(message));
        }

        public void WriteBlockEnd(String message)
        {
            Console.WriteLine("##teamcity[progressFinish '{0}']", Escape(message));
            Console.WriteLine("##teamcity[blockClosed name='{0}']", Escape(message));
        }

        public void WriteError(String message, Exception exception)
        {
            Console.WriteLine("##teamcity[message text='{0}' errorDetails='{1}' status='ERROR']", Escape(message), Escape(exception.StackTrace));
        }

        private static String Escape(String value)
        {
            return value == null 
                ? String.Empty 
                : value.Replace("|", "||").Replace("'", "|'").Replace("\r", "|r").Replace("\n", "|n").Replace("]", "|]");
        }
    }
}