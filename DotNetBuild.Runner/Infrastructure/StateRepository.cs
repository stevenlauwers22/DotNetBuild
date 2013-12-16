using System;
using System.Collections.Generic;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Infrastructure
{
    public class StateRepository
        : IStateReader, IStateWriter
    {
        private readonly IDictionary<string, object> _store;

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