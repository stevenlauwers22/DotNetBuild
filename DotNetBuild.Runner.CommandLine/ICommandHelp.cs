using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.CommandLine
{
    public interface ICommandHelp
    {
        void Print(ILogger logger);
    }
}