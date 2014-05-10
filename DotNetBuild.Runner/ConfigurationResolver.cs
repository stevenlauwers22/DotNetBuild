using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;

namespace DotNetBuild.Runner
{
    public interface IConfigurationResolver
    {
        IConfigurationSettings Resolve(String configurationName, IAssemblyWrapper assemblyWrapper);
    }

    public class ConfigurationResolver 
        : IConfigurationResolver
    {
        private readonly ITypeActivator _typeActivator;

        public ConfigurationResolver(ITypeActivator typeActivator)
        {
            if (typeActivator == null) 
                throw new ArgumentNullException("typeActivator");

            _typeActivator = typeActivator;
        }

        public IConfigurationSettings Resolve(String configurationName, IAssemblyWrapper assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            var configurationRegistryType = assembly.Get<IConfigurationRegistry>();
            if (configurationRegistryType == null)
                return null;

            var configurationRegistry = _typeActivator.Activate<IConfigurationRegistry>(configurationRegistryType);
            if (configurationRegistry == null)
                throw new UnableToActivateConfigurationRegistryException(configurationRegistryType);

            var configurationSettings = configurationRegistry.Get(configurationName);
            return configurationSettings;
        }
    }
}