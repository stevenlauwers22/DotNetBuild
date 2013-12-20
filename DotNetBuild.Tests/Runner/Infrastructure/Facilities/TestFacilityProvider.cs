using DotNetBuild.Runner.Infrastructure.Facilities;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities
{
    public class TestFacilityProvider
        : FacilityProvider<ITestFacilityAcceptor, ITestFacility>
    {
        private readonly ITestFacility _testFacility;

        public TestFacilityProvider(ILogger logger, ITestFacility testFacility)
            : base(logger)
        {
            _testFacility = testFacility;
        }

        protected override ITestFacility GetFacility()
        {
            return _testFacility;
        }
    }
}