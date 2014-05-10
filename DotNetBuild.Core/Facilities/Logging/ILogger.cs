using System;

namespace DotNetBuild.Core.Facilities.Logging
{
    public interface ILogger
        : IFacility
    {
        void LogInfo(String message);
        void LogError(String message, Exception exception);
    }
}