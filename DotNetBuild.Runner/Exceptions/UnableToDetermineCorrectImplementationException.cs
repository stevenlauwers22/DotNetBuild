using System;
using System.Collections.Generic;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToDetermineCorrectImplementationException 
        : DotNetBuildException
    {
        private readonly Type _type;
        private readonly IEnumerable<Type> _typeImplementations;

        public UnableToDetermineCorrectImplementationException(Type type, IEnumerable<Type> typeImplementations)
            : base(-18, string.Format("Implementation for type '{0}' could not be determined, multiple matching types were found", type.FullName))
        {
            _type = type;
            _typeImplementations = typeImplementations;
        }

        public Type Type
        {
            get { return _type; }
        }

        public IEnumerable<Type> TypeImplementations
        {
            get { return _typeImplementations; }
        }
    }
}