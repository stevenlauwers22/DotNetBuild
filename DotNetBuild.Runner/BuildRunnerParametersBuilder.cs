using System;

namespace DotNetBuild.Runner
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
            }

            var parameters = new BuildRunnerParameters(assembly, target, configuration);
            return parameters;
        }
    }
}