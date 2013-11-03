using System;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.CommandLine
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var container = ContainerConfiguration.CreateAndConfigure();
            var logger = container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {

                var commandLineInterpreter = container.Resolve<ICommandLineInterpreter>();
                var command = commandLineInterpreter.Interpret(args);
                if (command != null)
                {
                    logger.Write("Command parsed: " + command.GetType());

                    var commandDispatcher = container.Resolve<ICommandDispatcher>();
                    commandDispatcher.Dispatch(command);
                }
                else
                {
                    logger.Write("Command could not be understood due to invalid command line parameters.");
                    logger.Write("Usage of the application:");
                    PrintHelp(container, logger);
                }
            }
            catch (DotNetBuildException exception)
            {
                PrintExpectedException(exception, logger);
                return exception.ErrorCode;
            }
            catch (Exception exception)
            {
                PrintUnexpectedException(exception, logger);
                return -1;
            }
            finally
            {
                logger.Write("DotNetBuild finished");
            }

            return 0;
        }

        private static void PrintHelp(TinyIoCContainer container, ILogger logger)
        {
            var commandHelp = container.ResolveAll<ICommandHelp>();
            foreach (var ch in commandHelp)
            {
                ch.Print(logger);
            }
        }

        private static void PrintExpectedException(Exception exception, ILogger logger)
        {
            if (exception == null)
                return;

            logger.WriteError("An error occured while executing a DotNetBuild script", exception);
            PrintExpectedException(exception.InnerException, logger);
        }

        private static void PrintUnexpectedException(Exception exception, ILogger logger)
        {
            if (exception == null)
                return;

            logger.WriteError("An unexpected error occured while executing a DotNetBuild script", exception);
            PrintExpectedException(exception.InnerException, logger);
        }
    }
}
