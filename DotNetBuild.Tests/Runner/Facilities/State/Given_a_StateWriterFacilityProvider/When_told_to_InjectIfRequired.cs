using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateWriterFacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<StateWriterFacilityProvider>
    {
        private Mock<ILogger> _logger;
        private Mock<IStateWriter> _stateWriter;
        private StateWriterFacilityAcceptor _stateWriterFacilityAcceptor;

        protected override void Arrange()
        {
            _logger = new Mock<ILogger>();
            _stateWriter = new Mock<IStateWriter>();
            _stateWriterFacilityAcceptor = new StateWriterFacilityAcceptor();
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