namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToLoadAssemblyException 
        : DotNetBuildException
    {
        private readonly string _assembly;

        public UnableToLoadAssemblyException(string assembly)
            : base(-10, string.Format("Assembly with name '{0}' could not be loaded", assembly))
        {
            _assembly = assembly;
        }

        public string Assembly
        {
            get { return _assembly; }
        }
    }
}