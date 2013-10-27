using System;

namespace DotNetBuild.Runner.Infrastructure.Exceptions
{
    public class UnableToActivateConfigurationRegistryException 
        : DotNetBuildException
    {
        private readonly Type _configurationRegistryType;

        public UnableToActivateConfigurationRegistryException(Type configurationRegistryType)
            : base(-11, string.Format("Configuration registry of type '{0}' could not be activated", configurationRegistryType.FullName))
        {
            _configurationRegistryType = configurationRegistryType;
        }

        public Type ConfigurationRegistryType
        {
            get { return _configurationRegistryType; }
        }
    }
}