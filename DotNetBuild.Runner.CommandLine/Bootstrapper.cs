using DotNetBuild.Runner.CommandLine.StartBuild;
using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using DotNetBuild.Runner.StartBuild;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;

namespace DotNetBuild.Runner.CommandLine
{
    public class Bootstrapper
    {
        public static void Boot(TinyIoCContainer container)
        {
            container.Register<ICommandLineInterpreter, CommandLineInterpreter>();
            container.Register<ICommandBuilder, StartBuildCommandBuilder>("start-build");
            container.Register<ICommandHelp, StartBuildCommandHelp>("start-build-help");

            container.Register<ICommandDispatcher, CommandDispatcher>().UsingConstructor(() => new CommandDispatcher(container));
            container.Register<ICommandHandler<StartBuildCommand>, StartBuildHandler>();

            container.Register<IDomainEventDispatcher, DomainEventDispatcher>().UsingConstructor(() => new DomainEventDispatcher(container));
            container.Register<IDomainEventInitializer, DomainEventInitializer>().UsingConstructor(() => new DomainEventInitializer(@event => container.Resolve<IDomainEventDispatcher>().Dispatch(@event)));
            container.Register<IDomainEventHandler<BuildRequestedToStart>, BuildRequestedToStartHandler>();

            container.Register<IAssemblyLoader, AssemblyLoader>();
            container.Register<IBuildRunner, BuildRunner>();
            container.Register<IConfigurationResolver, ConfigurationResolver>();
            container.Register<IConfigurationSelector, ConfigurationSelector>();
            container.Register<ITargetExecutor, TargetExecutor>();
            container.Register<ITargetInspector, TargetInspector>();
            container.Register<ITargetResolver, TargetResolver>();
            container.Register<ITypeActivator, TypeActivator>();

            container.Register<IBuildRepository, BuildRepository>().AsSingleton();
        }
    }
}