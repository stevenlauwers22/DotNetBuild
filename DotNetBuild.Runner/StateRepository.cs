using System;
using System.Collections.Generic;

namespace DotNetBuild.Runner
{
    public interface IStateRepository
    {
        T Get<T>(string key);
        void Add(string key, object value);
    }

    public class StateRepository
        : IStateRepository
    {
        private static IDictionary<string, object> _store;

        public StateRepository()
        {
            _store = new Dictionary<string, object>();
        }

        public IEnumerable<KeyValuePair<string, object>> Store
        {
            get { return _store; }
        }

        public T Get<T>(string key)
        {
            if (!_store.ContainsKey(key))
                return default(T);

            var value = _store[key];
            return (T)value;
        }

        public void Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) 
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            _store[key] = value;
        }
    }
}