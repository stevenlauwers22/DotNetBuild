using System;

namespace DotNetBuild.Runner.Infrastructure.Exceptions
{
    public abstract class DotNetBuildException 
        : Exception
    {
        private readonly int _errorCode;

        protected DotNetBuildException(int errorCode, string message)
            : this(errorCode, message, null)
        {
        }

        protected DotNetBuildException(int errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            _errorCode = errorCode;
        }

        public int ErrorCode
        {
            get { return _errorCode; }
        }
    }
}