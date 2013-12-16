using System;
using DotNetBuild.Core.Facilities.State;

namespace DotNetBuild.Runner.Infrastructure.Facilities.State
{
    public class StateWriterFacilityProvider
        : FacilityProvider<IWantToWriteState, IStateWriter>
    {
        private readonly Func<IStateWriter> _stateWriterFunc;

        public StateWriterFacilityProvider(Func<IStateWriter> stateWriterFunc)
        {
            _stateWriterFunc = stateWriterFunc;
        }

        protected override IStateWriter GetFacility()
        {
            return _stateWriterFunc();
        }
    }
}