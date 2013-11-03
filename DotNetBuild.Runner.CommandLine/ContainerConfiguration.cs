using DotNetBuild.Runner.CommandLine.StartBuild;
using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using DotNetBuild.Runner.StartBuild;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;

namespace DotNetBuild.Runner.CommandLine
{
    public class ContainerConfiguration
    {
        public static TinyIoCContainer CreateAndConfigure()
        {
            var container = TinyIoCContainer.Current;

            container.Register(container);

            container.Register<ILoggerFactory, LoggerFactory>();
            container.Register((c, p) => c.Resolve<ILoggerFactory>().CreateLogger());

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
            container.Register<ITargetExecutor, TargetExecutor>();
            container.Register<ITargetInspector, TargetInspector>();
            container.Register<ITargetResolver, TargetResolver>();
            container.Register<ITypeActivator, TypeActivator>();

            container.Register<IBuildRepository, BuildRepository>().AsSingleton();

            return container;
        }
    }
}