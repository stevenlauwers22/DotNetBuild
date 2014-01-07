using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Facilities.State
{
    public class StateReader 
        : IStateReader
    {
        private readonly IStateRepository _stateRepository;

        public StateReader(IStateRepository stateRepository)
        {
            if (stateRepository == null) 
                throw new ArgumentNullException("stateRepository");

            _stateRepository = stateRepository;
        }

        public T Get<T>(string key)
        {
            return _stateRepository.Get<T>(key);
        }
    }
}