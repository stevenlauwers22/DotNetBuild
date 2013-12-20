using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities
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