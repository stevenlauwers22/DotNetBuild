using System;

namespace DotNetBuild.Runner.Exceptions
{
    public abstract class DotNetBuildException 
        : Exception
    {
        private readonly int _errorCode;

        protected DotNetBuildException(int errorCode, String message)
            : this(errorCode, message, null)
        {
        }

        protected DotNetBuildException(int errorCode, String message, Exception innerException)
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