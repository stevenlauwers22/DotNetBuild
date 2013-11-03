namespace DotNetBuild.Runner.Infrastructure.Logging
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }
}