using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Exceptions;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface IConfigurationResolver
    {
        IConfigurationSettings Resolve(string configurationName, IAssemblyWrapper assemblyWrapper);
    }

    public class ConfigurationResolver 
        : IConfigurationResolver
    {
        private readonly IConfigurationSelector _configurationSelector;
        private readonly ITypeActivator _typeActivator;

        public ConfigurationResolver(
            IConfigurationSelector configurationSelector,
            ITypeActivator typeActivator)
        {
            if (configurationSelector == null) 
                throw new ArgumentNullException("configurationSelector");

            if (typeActivator == null) 
                throw new ArgumentNullException("typeActivator");

            _configurationSelector = configurationSelector;
            _typeActivator = typeActivator;
        }

        public IConfigurationSettings Resolve(string configurationName, IAssemblyWrapper assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            var configurationRegistryType = assembly.Get<IConfigurationRegistry>();
            if (configurationRegistryType == null)
                return null;

            var configurationRegistry = _typeActivator.Activate<IConfigurationRegistry>(configurationRegistryType);
            if (configurationRegistry == null)
                throw new UnableToActivateConfigurationRegistryException(configurationRegistryType);

            var configurationSettings = _configurationSelector.Select(configurationName, configurationRegistry);
            return configurationSettings;
        }
    }
}