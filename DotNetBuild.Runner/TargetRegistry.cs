using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;

namespace DotNetBuild.Runner
{
    public class TargetRegistry : ITargetRegistry
    {
        private static IDictionary<String, ITarget> _registrations;

        public TargetRegistry()
        {
            _registrations = new Dictionary<String, ITarget>();
        }

        public IEnumerable<KeyValuePair<String, ITarget>> Registrations
        {
            get { return _registrations; }
        }

        public ITarget Get(String key)
        {
            var value = _registrations.SingleOrDefault(registration => String.Equals(registration.Key, key, StringComparison.OrdinalIgnoreCase)).Value;
            return value;
        }

        public void Add(String key, ITarget value)
        {
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _registrations[key] = value;
        }
    }
}