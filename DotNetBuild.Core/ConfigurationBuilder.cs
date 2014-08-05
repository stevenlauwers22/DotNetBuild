using System;

namespace DotNetBuild.Core
{
    public interface IConfigurationBuilder
    {
        IConfigurationBuilder AddSetting(String key, Object value);
    }

    public class ConfigurationBuilder
        : IConfigurationBuilder
    {
        private readonly IConfigurationRegistry _configurationRegistry;
        private readonly IConfigurationSettings _configurationSettings;

        public ConfigurationBuilder(IConfigurationRegistry configurationRegistry, String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            _configurationSettings = new ConfigurationSettings();
            _configurationRegistry = configurationRegistry;
            _configurationRegistry.Add(name, _configurationSettings);
        }

        public IConfigurationSettings GetSettings()
        {
            return _configurationSettings;
        }

        public IConfigurationBuilder AddSetting(String key, Object value)
        {
            _configurationSettings.Add(key, value);
            return this;
        }
    }
}