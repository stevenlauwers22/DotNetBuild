using DotNetBuild.Core.Facilities;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.Given_a_FacilityProvider
{
    public class When_told_to_InjectIfRequired_not_required
        : TestSpecification<TestFacilityProvider>
    {
        private TestFacility _testFacility;
        private Mock<IFacilityAcceptor<IFacility>> _testFacilityAcceptor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _testFacility = new TestFacility();
            _testFacilityAcceptor = new Mock<IFacilityAcceptor<IFacility>>();
            _logger = new Mock<ILogger>();
        }

        protected override TestFacilityProvider CreateSubjectUnderTest()
        {
            return new TestFacilityProvider(_logger.Object, _testFacility);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_testFacilityAcceptor.Object);
        }

        [Fact]
        public void Does_not_inject_the_facility()
        {
            _testFacilityAcceptor.Verify(fa => fa.Inject(_testFacility), Times.Never);
        }
    }
}