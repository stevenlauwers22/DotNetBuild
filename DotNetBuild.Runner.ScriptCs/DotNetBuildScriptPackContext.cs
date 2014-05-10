using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using DotNetBuild.Runner.Targets;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPackContext : IScriptPackContext
    {
        public int Run(String targetName, String configurationName)
        {
            var container = TinyIoCContainer.Current;
            var targetRegistry = container.Resolve<ITargetRegistry>();
            var target = targetRegistry.Get(targetName);
            return Run(target, null);
        }

        public int Run(ITarget target, IConfigurationSettings configurationSettings)
        {
            var container = TinyIoCContainer.Current;
            var logger = container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = container.Resolve<IBuildRunner>();
                buildRunner.Run(target, configurationSettings);
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
