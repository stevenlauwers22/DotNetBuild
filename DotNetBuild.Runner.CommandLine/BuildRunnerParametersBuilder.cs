using System;
using System.Collections.Generic;

namespace DotNetBuild.Runner.CommandLine
{
    public interface IBuildRunnerParametersBuilder
    {
        BuildRunnerParameters BuildFrom(String[] args);
    }
    public class BuildRunnerParametersBuilder
        : IBuildRunnerParametersBuilder
    {
        public BuildRunnerParameters BuildFrom(String[] args)
        {
            String assembly = null;
            String target = null;
            String configuration = null;
            var additionalParameters = new Dictionary<String, String>();
            foreach (var arg in args)
            {
                if (arg == null)
                    continue;

                if (arg.StartsWith(BuildRunnerParametersConstants.Assembly))
                {
                    assembly = arg.Substring(BuildRunnerParametersConstants.Assembly.Length);
                    continue;
                }

                if (arg.StartsWith(BuildRunnerParametersConstants.Target))
                {
                    target = arg.Substring(BuildRunnerParametersConstants.Target.Length);
                    continue;
                }

                if (arg.StartsWith(BuildRunnerParametersConstants.Configuration))
                {
                    configuration = arg.Substring(BuildRunnerParametersConstants.Configuration.Length);
                    continue;
                }

                var argSeparatorIndex = arg.IndexOf(":", StringComparison.InvariantCulture);
                if (argSeparatorIndex == -1)
                {
                    continue;
                }

                var argKey = arg.Substring(0, argSeparatorIndex);
                if (argKey.StartsWith("-"))
                    argKey = argKey.Remove(0, 1);

                var argValue = arg.Substring(argSeparatorIndex + 1);
                additionalParameters[argKey] = argValue;
            }

            var parameters = new BuildRunnerParameters(assembly, target, configuration, additionalParameters);
            return parameters;
        }
    }
}