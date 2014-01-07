using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Tests.Runner.Facilities
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