using System;

namespace DotNetBuild.Core
{
    public class ConfigurationRegistration
    {
        private readonly IConfigurationSettings _configurationSettings;
        private readonly Func<string, bool> _useIf;

        public ConfigurationRegistration(
            IConfigurationSettings configurationSettings,
            Func<string, bool> useIf)
        {
            if (configurationSettings == null)
                throw new ArgumentNullException("configurationSettings");

            if (useIf == null) 
                throw new ArgumentNullException("useIf");

            _configurationSettings = configurationSettings;
            _useIf = useIf;
        }

        public IConfigurationSettings Configuration
        {
            get { return _configurationSettings; }
        }

        public Func<string, bool> UseIf
        {
            get { return _useIf; }
        }
    }
}