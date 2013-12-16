using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<TestFacilityProvider>
    {
        private TestFacility _testFacility;
        private TestFacilityAcceptor _testFacilityAcceptor;

        protected override void Arrange()
        {
            _testFacility = new TestFacility();
            _testFacilityAcceptor = new TestFacilityAcceptor();
        }

        protected override TestFacilityProvider CreateSubjectUnderTest()
        {
            return new TestFacilityProvider(_testFacility);
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