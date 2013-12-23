using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Infrastructure.Facilities.State
{
    public class StateWriter 
        : IStateWriter
    {
        private readonly IStateRepository _stateRepository;

        public StateWriter(IStateRepository stateRepository)
        {
            if (stateRepository == null) 
                throw new ArgumentNullException("stateRepository");

            _stateRepository = stateRepository;
        }

        public void Add(string key, object value)
        {
            _stateRepository.Add(key, value);
        }
    }
}