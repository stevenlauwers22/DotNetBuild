﻿using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.State.Given_a_StateWriterFacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<StateWriterFacilityProvider>
    {
        private Mock<IStateWriter> _stateWriter;
        private StateWriterFacilityAcceptor _stateWriterFacilityAcceptor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _stateWriter = new Mock<IStateWriter>();
            _stateWriterFacilityAcceptor = new StateWriterFacilityAcceptor();
            _logger = new Mock<ILogger>();
        }

        protected override StateWriterFacilityProvider CreateSubjectUnderTest()
        {
            return new StateWriterFacilityProvider(_logger.Object, () => _stateWriter.Object);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_stateWriterFacilityAcceptor);
        }

        [Fact]
        public void Injects_the_facility()
        {
            Assert.Equal(_stateWriter.Object, _stateWriterFacilityAcceptor.Facility);
        }

        public class StateWriterFacilityAcceptor
            : IWantToWriteState
        {
            public IStateWriter Facility { get; private set; }

            public void Inject(IStateWriter facility)
            {
                Facility = facility;
            }
        }
    }
}