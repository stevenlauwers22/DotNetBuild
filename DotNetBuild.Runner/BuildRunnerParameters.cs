using System;
using System.Collections.Generic;

namespace DotNetBuild.Runner
{
    public class BuildRunnerParameters
    {
        private readonly String _assembly;
        private readonly String _target;
        private readonly String _configuration;
        private readonly IEnumerable<KeyValuePair<String, String>> _additionalParameters;

        public BuildRunnerParameters(String assembly, String target, String configuration, IEnumerable<KeyValuePair<String, String>> additionalParameters)
        {
            _assembly = assembly;
            _target = target;
            _configuration = configuration;
            _additionalParameters = additionalParameters;
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

        public IEnumerable<KeyValuePair<String, String>> AdditionalParameters
        {
            get { return _additionalParameters; }
        }
    }
}