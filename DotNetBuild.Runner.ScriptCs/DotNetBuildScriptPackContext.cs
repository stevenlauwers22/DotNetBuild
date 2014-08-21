﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using ScriptCs.Contracts;

namespace DotNetBuild.Runner.ScriptCs
{
    public class DotNetBuildScriptPackContext : IScriptPackContext
    {
        private readonly String[] _parameters;
        private readonly TinyIoCContainer _container;

        public DotNetBuildScriptPackContext(String[] parameters, TinyIoCContainer container)
        {
            _parameters = parameters;
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
            var parameterProvider = new ParameterProvider(_parameters);
            var target = parameterProvider.Get(ParameterConstants.Target);
            var configuration = parameterProvider.Get(ParameterConstants.Configuration);
            Run(target, configuration);
        }

        public void Run(String target, String configuration)
        {
            var logger = _container.Resolve<ILogger>();
            logger.Write("DotNetBuild started");

            try
            {
                var buildRunner = _container.Resolve<IBuildRunner>();
                buildRunner.Run(target, configuration, _parameters);
            }
            catch (DotNetBuildException exception)
            {
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
