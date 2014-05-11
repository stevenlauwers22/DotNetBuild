using System;
using System.Collections.Generic;

namespace DotNetBuild.Core.Targets
{
    public interface ITargetRegistry
    {
        ITarget Get(String key);
        void Add(String key, ITarget value);
    }

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
            if (!_registrations.ContainsKey(key))
                return null;

            var value = _registrations[key];
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