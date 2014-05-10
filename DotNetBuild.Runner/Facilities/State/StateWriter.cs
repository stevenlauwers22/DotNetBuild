using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Facilities.State
{
    public class StateWriter 
        : IStateWriter
    {
        private readonly IStateRegistry _stateRegistry;

        public StateWriter(IStateRegistry stateRegistry)
        {
            if (stateRegistry == null) 
                throw new ArgumentNullException("stateRegistry");

            _stateRegistry = stateRegistry;
        }

        public void Add(String key, object value)
        {
            _stateRegistry.Add(key, value);
        }
    }
}