using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Facilities.State
{
    public class StateReader 
        : IStateReader
    {
        private readonly IStateRegistry _stateRegistry;

        public StateReader(IStateRegistry stateRegistry)
        {
            if (stateRegistry == null) 
                throw new ArgumentNullException("stateRegistry");

            _stateRegistry = stateRegistry;
        }

        public T Get<T>(String key)
        {
            return _stateRegistry.Get<T>(key);
        }
    }
}