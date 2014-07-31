using DotNetBuild.Core;
using DotNetBuild.Core.Facilities;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Facilities;
using DotNetBuild.Runner.Facilities.Logging;
using DotNetBuild.Runner.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Reflection;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Configuration
{
    public class Container
    {
        public static void Install(TinyIoCContainer container)
        {
            container.Register(container);

            container.Register<Infrastructure.Logging.ILoggerFactory, Infrastructure.Logging.LoggerFactory>();
            container.Register((c, p) => c.Resolve<Infrastructure.Logging.ILoggerFactory>().CreateLogger());

            container.Register<IAssemblyLoader, AssemblyLoader>();
            container.Register<ITypeActivator, TypeActivator>();

            container.Register<IBuildRunner, BuildRunner>();
            container.Register<IBuildRunnerParametersBuilder, BuildRunnerParametersBuilder>();
            container.Register<IBuildRunnerParametersHelp, BuildRunnerParametersHelp>();

            container.Register<IConfiguratorResolver, ConfiguratorResolver>();
            container.Register<IConfigurationRegistry, ConfigurationRegistry>();
            ConfigurationBuilderExtension.ResolveConfigurationRegistry(container.Resolve<IConfigurationRegistry>);

            container.Register<ITargetExecutor, TargetExecutor>();
            container.Register<ITargetInspector, TargetInspector>();
            container.Register<ITargetRegistry, TargetRegistry>();
            TargetBuilderExtension.ResolveTargetRegistry(container.Resolve<ITargetRegistry>);

            container.Register<ILogger, Logger>();
            container.Register<IStateRegistry, StateRegistry>();
            container.Register<IStateReader, StateReader>();
            container.Register<IStateWriter, StateWriter>();
            container.Register<IFacilityProvider>((c, p) => new FacilityProvider(new IFacility[]
            {
                c.Resolve<ILogger>(),
                c.Resolve<IStateReader>(),
                c.Resolve<IStateWriter>()
            }));
        }
    }
}