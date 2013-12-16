using DotNetBuild.Runner.Infrastructure.Facilities;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public class TestFacilityProvider
        : FacilityProvider<ITestFacilityAcceptor, ITestFacility>
    {
        private readonly ITestFacility _testFacility;

        public TestFacilityProvider(ITestFacility testFacility)
        {
            _testFacility = testFacility;
        }

        protected override ITestFacility GetFacility()
        {
            return _testFacility;
        }
    }
}