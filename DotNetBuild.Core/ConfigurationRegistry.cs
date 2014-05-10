using System;
using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface IConfigurationRegistry
    {
        IConfigurationSettings Get(String key);
    }

    public abstract class ConfigurationRegistry : IConfigurationRegistry
    {
        private static IDictionary<String, IConfigurationSettings> _registrations;

        protected ConfigurationRegistry()
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

        protected void Add(String key, IConfigurationSettings value)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _registrations[key] = value;
        }
    }
}