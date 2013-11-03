using System;

namespace DotNetBuild.Runner.Infrastructure.Logging.Loggers
{
    public class StandardLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteBlockStart(string message)
        {
            Console.WriteLine("START: " + message);
            Console.WriteLine("------------------");
        }

        public void WriteBlockEnd(string message)
        {
            Console.WriteLine("END: " + message);
            Console.WriteLine("------------------");
        }

        public void WriteError(string message, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Empty);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            Console.WriteLine("------------------");
            Console.ResetColor();
        }
    }
}