using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Facilities.State;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.State.Given_a_StateReaderFacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<StateReaderFacilityProvider>
    {
        private Mock<ILogger> _logger;
        private Mock<IStateReader> _stateReader;
        private StateReaderFacilityAcceptor _stateReaderFacilityAcceptor;

        protected override void Arrange()
        {
            _logger = new Mock<ILogger>();
            _stateReader = new Mock<IStateReader>();
            _stateReaderFacilityAcceptor = new StateReaderFacilityAcceptor();
        }

        protected override StateReaderFacilityProvider CreateSubjectUnderTest()
        {
            return new StateReaderFacilityProvider(_logger.Object, () => _stateReader.Object);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_stateReaderFacilityAcceptor);
        }

        [Fact]
        public void Injects_the_facility()
        {
            Assert.Equal(_stateReader.Object, _stateReaderFacilityAcceptor.Facility);
        }

        public class StateReaderFacilityAcceptor
            : IWantToReadState
        {
            public IStateReader Facility { get; private set; }

            public void Inject(IStateReader facility)
            {
                Facility = facility;
            }
        }
    }
}