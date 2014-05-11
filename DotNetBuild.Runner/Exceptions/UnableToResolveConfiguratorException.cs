using System;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToResolveConfiguratorException
        : DotNetBuildException
    {
        private readonly String _assembly;

        public UnableToResolveConfiguratorException(String assembly)
            : base(-11, String.Format("An implementation of IConfigurator could not be found in assembly '{0}'", assembly))
        {
            _assembly = assembly;
        }

        public String Assembly
        {
            get { return _assembly; }
        }
    }
}