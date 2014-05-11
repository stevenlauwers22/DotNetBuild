using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;

namespace DotNetBuild.Runner
{
    public interface IConfiguratorResolver
    {
        IConfigurator Resolve(IAssemblyWrapper assembly);
    }

    public class ConfiguratorResolver
        : IConfiguratorResolver
    {
        private readonly ITypeActivator _typeActivator;

        public ConfiguratorResolver(ITypeActivator typeActivator)
        {
            if (typeActivator == null)
                throw new ArgumentNullException("typeActivator");

            _typeActivator = typeActivator;
        }

        public IConfigurator Resolve(IAssemblyWrapper assembly)
        {
            if (assembly == null) 
                throw new ArgumentNullException("assembly");

            var configuratorType = assembly.Get<IConfigurator>();
            if (configuratorType == null)
                throw new UnableToResolveConfiguratorException(assembly.Assembly.FullName);

            var configurator = _typeActivator.Activate<IConfigurator>(configuratorType);
            if (configurator == null)
                throw new UnableToActivateConfiguratorException(configuratorType);

            return configurator;
        }
    }
}