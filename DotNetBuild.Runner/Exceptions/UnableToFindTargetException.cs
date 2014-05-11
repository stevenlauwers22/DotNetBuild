using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToFindTargetException
        : DotNetBuildException
    {
        private readonly String _target;

        public UnableToFindTargetException(String target)
            : base(-13, String.Format("Target with name '{0}' could not be found", target))
        {
            _target = target;
        }

        public String Target
        {
            get { return _target; }
        }
    }
}