using DotNetBuild.Core.Facilities;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public class When_told_to_InjectIfRequired_not_required
        : TestSpecification<TestFacilityProvider>
    {
        private TestFacility _testFacility;
        private Mock<IFacilityAcceptor<IFacility>> _testFacilityAcceptor;

        protected override void Arrange()
        {
            _testFacility = new TestFacility();
            _testFacilityAcceptor = new Mock<IFacilityAcceptor<IFacility>>();
        }

        protected override TestFacilityProvider CreateSubjectUnderTest()
        {
            return new TestFacilityProvider(_testFacility);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_testFacilityAcceptor.Object);
        }

        [Fact]
        public void Wraps_the_assembly()
        {
            _testFacilityAcceptor.Verify(fa => fa.Inject(_testFacility), Times.Never);
        }
    }
}