using System;

namespace DotNetBuild.Runner.Infrastructure.Logging.Loggers
{
    public class StandardLogger : ILogger
    {
        public void Write(String message)
        {
            Console.WriteLine(message);
        }

        public void WriteBlockStart(String message)
        {
            Console.WriteLine("START: " + message);
            Console.WriteLine("------------------");
        }

        public void WriteBlockEnd(String message)
        {
            Console.WriteLine("END: " + message);
            Console.WriteLine("------------------");
        }

        public void WriteError(String message, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Empty);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            Console.WriteLine("------------------");
            Console.ResetColor();
        }
    }
}