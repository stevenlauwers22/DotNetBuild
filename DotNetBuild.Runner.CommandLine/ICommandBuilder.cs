using DotNetBuild.Runner.Infrastructure.Commands;

namespace DotNetBuild.Runner.CommandLine
{
    public interface ICommandBuilder
    {
        ICommand BuildFrom(string[] args);
    }
}