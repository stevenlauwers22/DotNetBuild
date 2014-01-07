using System;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.Facilities.State
{
    public class StateReaderFacilityProvider
        : FacilityProvider<IWantToReadState, IStateReader>
    {
        private readonly Func<IStateReader> _stateReaderFunc;
        
        public StateReaderFacilityProvider(ILogger logger, Func<IStateReader> stateReaderFunc)
            : base(logger)
        {
            _stateReaderFunc = stateReaderFunc;
        }

        protected override IStateReader GetFacility()
        {
            return _stateReaderFunc();
        }
    }
}