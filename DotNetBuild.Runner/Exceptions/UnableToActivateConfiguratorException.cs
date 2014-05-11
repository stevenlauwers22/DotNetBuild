using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToActivateConfiguratorException 
        : DotNetBuildException
    {
        private readonly Type _configuratorType;

        public UnableToActivateConfiguratorException(Type configuratorType)
            : base(-12, String.Format("Configurator of type '{0}' could not be activated", configuratorType.FullName))
        {
            _configuratorType = configuratorType;
        }

        public Type ConfiguratorType
        {
            get { return _configuratorType; }
        }
    }
}