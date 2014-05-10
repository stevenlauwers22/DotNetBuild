using System;
using DotNetBuild.Runner.Configuration;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.CommandLine
{
    public class DotNetBuild
    {
        public static int Main(String[] args)
        {
            var container = TinyIoCContainer.Current;
            Container.Install(container);

            var logger = container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunnerParametersBuilder = new BuildRunnerParametersBuilder();
                var buildRunnerParameters = buildRunnerParametersBuilder.BuildFrom(args);
                if (buildRunnerParameters != null)
                {
                    logger.Write("Command parsed: " + buildRunnerParameters.GetType());
                    
                    var buildRunner = container.Resolve<IBuildRunner>();
                    buildRunner.Run(buildRunnerParameters.Assembly, buildRunnerParameters.Target, buildRunnerParameters.Configuration);
                }
                else
                {
                    logger.Write("Command could not be understood due to invalid command line parameters.");
                    logger.Write("Usage of the application:");
                    PrintHelp(logger);
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

        private static void PrintHelp(ILogger logger)
        {
            var commandHelp = new BuildRunnerParametersHelp();
            commandHelp.Print(logger);
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
