using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToExecuteTargetException 
        : DotNetBuildException
    {
        private readonly Type _targetType;

        public UnableToExecuteTargetException(Type targetType)
            : this(targetType, null)
        {
        }

        public UnableToExecuteTargetException(Type targetType, Exception innerException)
            : base(-16, String.Format("An error occured while executing target '{0}'", targetType.FullName), innerException)
        {
            _targetType = targetType;
        }

        public Type TargetType
        {
            get { return _targetType; }
        }
    }
}