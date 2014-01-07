using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToActivateTypeWithNoDefaultConstructorException 
        : DotNetBuildException
    {
        private readonly Type _type;

        public UnableToActivateTypeWithNoDefaultConstructorException(Type type)
            : base(-15, string.Format("Type '{0}' could not be activated, no default constructor was found", type.Name))
        {
            _type = type;
        }

        public Type Type
        {
            get { return _type; }
        }
    }
}