using System;

namespace DotNetBuild.Runner.Infrastructure.Logging.Loggers
{
    public class StandardLogger
        : ILogger
    {
        public void Write(String message)
        {
            Console.WriteLine(message);
        }

        public void WriteBlockStart(String message)
        {
            Console.WriteLine("START: " + message);
        }

        public void WriteBlockEnd(String message)
        {
            Console.WriteLine("END: " + message);
        }

        public void WriteError(String message, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(String.Empty);
            Console.Error.WriteLine(exception.Message);
            Console.Error.WriteLine(exception.StackTrace);
            Console.Error.WriteLine("------------------");
            Console.ResetColor();
        }
    }
}