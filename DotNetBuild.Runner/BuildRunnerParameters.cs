using System.Collections.Generic;

namespace DotNetBuild.Runner
{
    public class BuildRunnerParameters
    {
        private readonly string _assembly;
        private readonly string _target;
        private readonly string _configuration;
        private readonly IEnumerable<KeyValuePair<string, string>> _additionalParameters;

        public BuildRunnerParameters(string assembly, string target, string configuration, IEnumerable<KeyValuePair<string, string>> additionalParameters)
        {
            _assembly = assembly;
            _target = target;
            _configuration = configuration;
            _additionalParameters = additionalParameters;
        }

        public string Assembly
        {
            get { return _assembly; }
        }

        public string Target
        {
            get { return _target; }
        }

        public string Configuration
        {
            get { return _configuration; }
        }

        public IEnumerable<KeyValuePair<string, string>> AdditionalParameters
        {
            get { return _additionalParameters; }
        }
    }
}