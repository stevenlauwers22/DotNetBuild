using System;

namespace DotNetBuild.Runner.Infrastructure.Logging
{
    public interface ILogger
    {
        void Write(string message);
        void WriteBlockStart(string message);
        void WriteBlockEnd(string message);
        void WriteError(string message, Exception exception);
    }
}