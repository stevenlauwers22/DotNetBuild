using System;
using System.Collections.Generic;

namespace DotNetBuild.Runner.Facilities.State
{
    public interface IStateRegistry
    {
        T Get<T>(String key);
        void Add(String key, object value);
    }

    public class StateRegistry
        : IStateRegistry
    {
        private static IDictionary<String, object> _registrations;

        public StateRegistry()
        {
            _registrations = new Dictionary<String, object>();
        }

        public IEnumerable<KeyValuePair<String, object>> Registrations
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

        public void Add(String key, object value)
        {
            if (String.IsNullOrEmpty(key)) 
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _registrations[key] = value;
        }
    }
}