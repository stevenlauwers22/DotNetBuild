using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Core.Targets;
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
            container.Register<IConfiguratorResolver, ConfiguratorResolver>();
            container.Register<ITargetExecutor, TargetExecutor>();
            container.Register<ITargetInspector, TargetInspector>();
            container.Register<ITargetRegistry, TargetRegistry>();

            container.Register<ILogger, Logger>();
            container.Register<IStateRegistry, StateRegistry>();
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
        }
    }
}