using System;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPackContext : IScriptPackContext
    {
        public void Configure(Action configuration)
        {
            configuration();
        }

        public int Run(String targetName, String configurationName)
        {
            var container = TinyIoCContainer.Current;
            var logger = container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = container.Resolve<IBuildRunner>();
                buildRunner.Run(targetName, configurationName);
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
