using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Infrastructure.Facilities.State
{
    public class StateReaderFacilityProvider
        : FacilityProvider<IWantToReadState, IStateReader>
    {
        private readonly Func<IStateReader> _stateReaderFunc;

        public StateReaderFacilityProvider(Func<IStateReader> stateReaderFunc)
        {
            _stateReaderFunc = stateReaderFunc;
        }

        protected override IStateReader GetFacility()
        {
            return _stateReaderFunc();
        }
    }
}