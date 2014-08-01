using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPackContext : IScriptPackContext
    {
        private readonly String[] _args;
        private readonly TinyIoCContainer _container;

        public DotNetBuildScriptPackContext(String[] args, TinyIoCContainer container)
        {
            _args = args;
            _container = container;
        }

        public void AddTarget(String name, String description, Action<ITargetBuilder> targetConfigurator)
        {
            var targetBuilder = name.Target(description);
            targetConfigurator(targetBuilder);
        }

        public void AddTarget(String name, ITarget target)
        {
            name.Target(target);
        }

        public void AddConfiguration(String name, Action<IConfigurationBuilder> settingsConfigurator)
        {
            var settings = name.Configure();
            settingsConfigurator(settings);
        }

        public void AddConfiguration(String name, IConfigurationSettings settings)
        {
            name.Configure(settings);
        }

        public void RunFromScriptArguments()
        {
            var buildRunnerParametersBuilder = _container.Resolve<IBuildRunnerParametersReader>();
            var target = buildRunnerParametersBuilder.Read(BuildRunnerParametersConstants.Target, _args);
            var configuration = buildRunnerParametersBuilder.Read(BuildRunnerParametersConstants.Configuration, _args);
            Run(target, configuration);
        }

        public void Run(String target, String configuration)
        {
            var logger = _container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = _container.Resolve<IBuildRunner>();
                buildRunner.Run(target, configuration);
            }
            catch (DotNetBuildException exception)
            {
                PrintHelp(logger);
                PrintExpectedException(exception, logger);
                throw;
            }
            catch (Exception exception)
            {
                PrintUnexpectedException(exception, logger);
                throw;
            }
            finally
            {
                logger.Write("DotNetBuild finished");
            }
        }

        private static void PrintHelp(ILogger logger)
        {
            logger.Write("Usage of the application:");

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
