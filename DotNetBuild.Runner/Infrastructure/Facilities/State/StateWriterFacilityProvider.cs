using System;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.Infrastructure.Facilities.State
{
    public class StateWriterFacilityProvider
        : FacilityProvider<IWantToWriteState, IStateWriter>
    {
        private readonly Func<IStateWriter> _stateWriterFunc;

        public StateWriterFacilityProvider(ILogger logger, Func<IStateWriter> stateWriterFunc)
            : base(logger)
        {
            _stateWriterFunc = stateWriterFunc;
        }

        protected override IStateWriter GetFacility()
        {
            return _stateWriterFunc();
        }
    }
}