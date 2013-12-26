using System;

namespace DotNetBuild.Core.Facilities.Logging
{
    public interface ILogger
        : IFacility
    {
        void LogInfo(string message);
        void LogError(string message, Exception exception);
    }
}