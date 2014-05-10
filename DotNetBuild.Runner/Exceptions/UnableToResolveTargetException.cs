using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToResolveTargetException 
        : DotNetBuildException
    {
        private readonly String _target;
        private readonly String _assembly;

        public UnableToResolveTargetException(String target, String assembly)
            : base(-13, String.Format("Target with name '{0}' could not be found in assembly '{1}'", target, assembly))
        {
            _target = target;
            _assembly = assembly;
        }

        public String Target
        {
            get { return _target; }
        }

        public String Assembly
        {
            get { return _assembly; }
        }
    }
}