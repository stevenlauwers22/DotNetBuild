using System;

namespace DotNetBuild.Runner
{
    public interface IBuildRunnerParametersReader
    {
        String Read(String parameterToRead, String[] args);
    }
    public class BuildRunnerParametersReader
        : IBuildRunnerParametersReader
    {
        public String Read(String parameterToRead, String[] args)
        {
            foreach (var arg in args)
            {
                if (arg == null)
                    continue;

                if (arg.StartsWith(parameterToRead))
                {
                    var parameter = arg.Substring(parameterToRead.Length);
                    return parameter;
                }
            }

            return null;
        }
    }
}