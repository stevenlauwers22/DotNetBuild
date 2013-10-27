using System;

namespace DotNetBuild.Runner.Infrastructure.Exceptions
{
    public class UnableToActivateTargetException 
        : DotNetBuildException
    {
        private readonly Type _targetType;

        public UnableToActivateTargetException(Type targetType)
            : base(-14, string.Format("Target of type '{0}' could not be activated", targetType.FullName))
        {
            _targetType = targetType;
        }

        public Type TargetType
        {
            get { return _targetType; }
        }
    }
}