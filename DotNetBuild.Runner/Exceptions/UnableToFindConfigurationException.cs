using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToFindConfigurationException
        : DotNetBuildException
    {
        private readonly String _configuration;

        public UnableToFindConfigurationException(String configuration)
            : base(-13, String.Format("Configuration with name '{0}' could not be found", configuration))
        {
            _configuration = configuration;
        }

        public String Configuration
        {
            get { return _configuration; }
        }
    }
}