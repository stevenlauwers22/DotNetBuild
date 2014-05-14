using System;
using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface IConfigurationSettings
    {
        T Get<T>(String key);
        void Add(String key, Object value);
    }

    public class ConfigurationSettings : IConfigurationSettings
    {
        private readonly IDictionary<String, Object> _registrations;

        public ConfigurationSettings()
        {
            _registrations = new Dictionary<String, Object>();
        }

        public IEnumerable<KeyValuePair<String, Object>> Registrations
        {
            get { return _registrations; }
        }

        public T Get<T>(String key)
        {
            if (!_registrations.ContainsKey(key))
                return default(T);

            var value = _registrations[key];
            return (T)value;
        }

        public void Add(String key, Object value)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _registrations[key] = value;
        }
    }
}