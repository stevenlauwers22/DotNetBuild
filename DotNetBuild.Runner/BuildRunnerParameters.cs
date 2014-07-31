using System;

namespace DotNetBuild.Runner
{
    public class BuildRunnerParameters
    {
        private readonly String _assembly;
        private readonly String _target;
        private readonly String _configuration;

        public BuildRunnerParameters(String assembly, String target, String configuration)
        {
            _assembly = assembly;
            _target = target;
            _configuration = configuration;
        }

        public String Assembly
        {
            get { return _assembly; }
        }

        public String Target
        {
            get { return _target; }
        }

        public String Configuration
        {
            get { return _configuration; }
        }
    }
}