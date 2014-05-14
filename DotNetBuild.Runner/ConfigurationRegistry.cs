using System;
using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Runner
{
    public class ConfigurationRegistry : IConfigurationRegistry
    {
        private static IDictionary<String, IConfigurationSettings> _registrations;

        public ConfigurationRegistry()
        {
            _registrations = new Dictionary<string, IConfigurationSettings>();
        }

        public IEnumerable<KeyValuePair<String, IConfigurationSettings>> Registrations
        {
            get { return _registrations; }
        }

        public IConfigurationSettings Get(String key)
        {
            if (!_registrations.ContainsKey(key))
                return null;

            var value = _registrations[key];
            return value;
        }

        public void Add(String key, IConfigurationSettings value)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _registrations[key] = value;
        }
    }
}