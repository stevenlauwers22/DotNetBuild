using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToLoadAssemblyException 
        : DotNetBuildException
    {
        private readonly String _assembly;

        public UnableToLoadAssemblyException(String assembly)
            : base(-10, String.Format("Assembly with name '{0}' could not be loaded", assembly))
        {
            _assembly = assembly;
        }

        public String Assembly
        {
            get { return _assembly; }
        }
    }
}