using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface IConfigurationSettings
    {
        T Get<T>(string key);
    }

    public abstract class ConfigurationSettings : IConfigurationSettings
    {
        private readonly IDictionary<string, object> _settings;

        protected ConfigurationSettings()
        {
            _settings = new Dictionary<string, object>();
        }

        protected void Add(string key, object value)
        {
            _settings[key] = value;
        }

        public T Get<T>(string key)
        {
            var settingPresent = _settings.ContainsKey(key);
            if (!settingPresent)
            {
                return default(T);
            }

            var value = _settings[key];
            return (T)value;
        }
    }
}