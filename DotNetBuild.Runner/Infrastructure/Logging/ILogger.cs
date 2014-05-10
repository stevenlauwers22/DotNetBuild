using System;

namespace DotNetBuild.Runner.Infrastructure.Logging
{
    public interface ILogger
    {
        void Write(String message);
        void WriteBlockStart(String message);
        void WriteBlockEnd(String message);
        void WriteError(String message, Exception exception);
    }
}