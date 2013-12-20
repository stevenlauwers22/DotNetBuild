using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<TestFacilityProvider>
    {
        private TestFacility _testFacility;
        private TestFacilityAcceptor _testFacilityAcceptor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _testFacility = new TestFacility();
            _testFacilityAcceptor = new TestFacilityAcceptor();
            _logger = new Mock<ILogger>();
        }

        protected override TestFacilityProvider CreateSubjectUnderTest()
        {
            return new TestFacilityProvider(_logger.Object, _testFacility);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_testFacilityAcceptor);
        }

        [Fact]
        public void Injects_the_facility()
        {
            Assert.Equal(_testFacility, _testFacilityAcceptor.TestFacility);
        }
    }
}