using System;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Assembly
{
    public class DotNetBuild
    {
        private readonly String[] _args;
        private readonly TinyIoCContainer _container;

        public DotNetBuild(String[] args, TinyIoCContainer container)
        {
            _args = args;
            _container = container;
        }

        public int Run()
        {
            var buildRunnerParametersBuilder = _container.Resolve<IBuildRunnerParametersReader>();
            var assembly = buildRunnerParametersBuilder.Read(BuildRunnerParametersConstants.Assembly, _args);
            var target = buildRunnerParametersBuilder.Read(BuildRunnerParametersConstants.Target, _args);
            var configuration = buildRunnerParametersBuilder.Read(BuildRunnerParametersConstants.Configuration, _args);
            var logger = _container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = _container.Resolve<IBuildRunner>();
                buildRunner.Run(assembly, target, configuration);
            }
            catch (DotNetBuildException exception)
            {
                PrintHelp(logger);
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

        private void PrintHelp(ILogger logger)
        {
            logger.Write("Usage of the application:");

            var commandHelp = _container.Resolve<IBuildRunnerParametersHelp>();
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
