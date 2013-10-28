using System;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.CommandLine
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var container = TinyIoCContainer.Current;
                Bootstrapper.Boot(container);

                var commandLineInterpreter = container.Resolve<ICommandLineInterpreter>();
                var command = commandLineInterpreter.Interpret(args);
                if (command != null)
                {
                    var commandDispatcher = container.Resolve<ICommandDispatcher>();
                    commandDispatcher.Dispatch(command);
                }
                else
                {
                    PrintHelp(container);
                }
            }
            catch (DotNetBuildException exception)
            {
                PrintException(exception);
                return exception.ErrorCode;
            }
            catch (Exception exception)
            {
                PrintException(exception);
                return -1;
            }

            return 0;
        }

        private static void PrintHelp(TinyIoCContainer container)
        {
            var commandHelp = container.ResolveAll<ICommandHelp>();
            foreach (var ch in commandHelp)
            {
                ch.Print();
            }
        }

        private static void PrintException(Exception exception)
        {
            if (exception == null)
                return;

            Console.WriteLine();
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            Console.WriteLine("------------------");
            PrintException(exception.InnerException);
        }
    }
}
