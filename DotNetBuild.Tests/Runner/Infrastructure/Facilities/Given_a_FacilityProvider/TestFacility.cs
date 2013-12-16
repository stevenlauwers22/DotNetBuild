using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public interface ITestFacility
        : IFacility
    {
    }

    public class TestFacility 
        : ITestFacility
    {
    }
}