namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToResolveTargetException 
        : DotNetBuildException
    {
        private readonly string _assembly;

        public UnableToResolveTargetException(string target, string assembly)
            : base(-13, string.Format("Target with name '{0}' could not be found in assembly '{1}'", target, assembly))
        {
            _assembly = assembly;
        }

        public string Assembly
        {
            get { return _assembly; }
        }
    }
}