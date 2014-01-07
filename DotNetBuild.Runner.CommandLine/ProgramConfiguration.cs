﻿using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.CommandLine.StartBuild;
using DotNetBuild.Runner.Facilities;
using DotNetBuild.Runner.Facilities.Logging;
using DotNetBuild.Runner.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using DotNetBuild.Runner.StartBuild;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;

namespace DotNetBuild.Runner.CommandLine
{
    public class ProgramConfiguration
    {
        public static TinyIoCContainer ConfigureContainer()
        {
            var container = TinyIoCContainer.Current;

            container.Register(container);

            container.Register<Infrastructure.Logging.ILoggerFactory, Infrastructure.Logging.LoggerFactory>();
            container.Register((c, p) => c.Resolve<Infrastructure.Logging.ILoggerFactory>().CreateLogger());

            container.Register<IDomainEventDispatcher, DomainEventDispatcher>();
            container.Register<IDomainEventInitializer>((c, p) => new DomainEventInitializer(@event => c.Resolve<IDomainEventDispatcher>().Dispatch(@event)));
            container.Register<IDomainEventHandler<BuildRequestedToStart>, BuildRequestedToStartHandler>();

            container.Register<ICommandLineInterpreter, CommandLineInterpreter>();
            container.Register<ICommandBuilder, StartBuildCommandBuilder>("start-build");
            container.Register<ICommandHelp, StartBuildCommandHelp>("start-build-help");

            container.Register<ICommandDispatcher, CommandDispatcher>();
            container.Register<ICommandHandler<StartBuildCommand>, StartBuildHandler>();

            container.Register<IAssemblyLoader, AssemblyLoader>();
            container.Register<IBuildRunner, BuildRunner>();
            container.Register<IConfigurationResolver, ConfigurationResolver>();
            container.Register<IConfigurationSelector, ConfigurationSelector>();
            container.Register<IStateRepository, StateRepository>();
            container.Register<ITargetExecutor, TargetExecutor>();
            container.Register<ITargetInspector, TargetInspector>();
            container.Register<ITargetResolver, TargetResolver>();
            container.Register<ITypeActivator, TypeActivator>();

            container.Register<ILogger, Logger>();
            container.Register<IStateReader, StateReader>();
            container.Register<IStateWriter, StateWriter>();
            container.Register((c, p) => new LoggerFacilityProvider(c.Resolve<Infrastructure.Logging.ILogger>(), c.Resolve<ILogger>));
            container.Register((c, p) => new StateReaderFacilityProvider(c.Resolve<Infrastructure.Logging.ILogger>(), c.Resolve<IStateReader>));
            container.Register((c, p) => new StateWriterFacilityProvider(c.Resolve<Infrastructure.Logging.ILogger>(), c.Resolve<IStateWriter>));
            container.RegisterMultiple<IFacilityProvider>(new []
            {
                typeof (LoggerFacilityProvider),
                typeof (StateReaderFacilityProvider),
                typeof (StateWriterFacilityProvider)
            });

            container.Register<IBuildRepository, BuildRepository>();

            return container;
        }
    }
}