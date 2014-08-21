using System;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Assembly
{
    public class DotNetBuild
    {
        private readonly String[] _parameters;
        private readonly TinyIoCContainer _container;

        public DotNetBuild(String[] parameters, TinyIoCContainer container)
        {
            _parameters = parameters;
            _container = container;
        }

        public int Run()
        {
            var parameterProvider = new ParameterProvider(_parameters);
            var assembly = parameterProvider.Get(ParameterConstants.Assembly);
            var target = parameterProvider.Get(ParameterConstants.Target);
            var configuration = parameterProvider.Get(ParameterConstants.Configuration);
            var logger = _container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = _container.Resolve<IBuildRunner>();
                buildRunner.Run(assembly, target, configuration, _parameters);
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
