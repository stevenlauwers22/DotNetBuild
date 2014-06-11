using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Core
{
    public class TargetExecutionContext
    {
        private readonly IConfigurationSettings _configurationSettings;
        private readonly IFacilityProvider _facilityProvider;

        public TargetExecutionContext(
            IConfigurationSettings configurationSettings, 
            IFacilityProvider facilityProvider)
        {
            _configurationSettings = configurationSettings;
            _facilityProvider = facilityProvider;
        }

        public IConfigurationSettings ConfigurationSettings
        {
            get { return _configurationSettings; }
        }

        public IFacilityProvider FacilityProvider
        {
            get { return _facilityProvider; }
        }
    }
}