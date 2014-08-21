using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Core
{
    public class TargetExecutionContext
    {
        private readonly IConfigurationSettings _configurationSettings;
        private readonly IParameterProvider _parameterProvider;
        private readonly IFacilityProvider _facilityProvider;

        public TargetExecutionContext(
            IConfigurationSettings configurationSettings, 
            IParameterProvider parameterProvider,
            IFacilityProvider facilityProvider)
        {
            _configurationSettings = configurationSettings;
            _parameterProvider = parameterProvider;
            _facilityProvider = facilityProvider;
        }

        public IConfigurationSettings ConfigurationSettings
        {
            get { return _configurationSettings; }
        }

        public IParameterProvider ParameterProvider
        {
            get { return _parameterProvider; }
        }

        public IFacilityProvider FacilityProvider
        {
            get { return _facilityProvider; }
        }
    }
}