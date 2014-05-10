using System;
using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface IConfigurationSettings
    {
        T Get<T>(String key);
    }

    public abstract class ConfigurationSettings : IConfigurationSettings
    {
        private readonly IDictionary<String, object> _settings;

        protected ConfigurationSettings()
        {
            _settings = new Dictionary<String, object>();
        }

        protected void Add(String key, object value)
        {
            _settings[key] = value;
        }

        public T Get<T>(String key)
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