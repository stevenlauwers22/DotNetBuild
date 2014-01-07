using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToDetermineCorrectConfigurationSettingsException 
        : DotNetBuildException
    {
        private readonly IEnumerable<IConfigurationSettings> _matchingConfigurationSettings;

        public UnableToDetermineCorrectConfigurationSettingsException(IEnumerable<IConfigurationSettings> matchingConfigurationSettings)
            : base(-12, string.Format("Configuration settings could not be determined, multiple matching configuration settings were found"))
        {
            _matchingConfigurationSettings = matchingConfigurationSettings;
        }

        public IEnumerable<IConfigurationSettings> MatchingConfigurationSettings
        {
            get { return _matchingConfigurationSettings; }
        }
    }
}