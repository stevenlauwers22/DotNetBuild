using System;
using System.Collections.Generic;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.StartBuild;

namespace DotNetBuild.Runner.CommandLine.StartBuild
{
    public class StartBuildCommandBuilder
        : ICommandBuilder
    {
        public ICommand BuildFrom(string[] args)
        {
            string assembly = null;
            string target = null;
            string configuration = null;
            var additionalParameters = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                if (arg == null)
                    continue;

                if (arg.StartsWith(StartBuildCommandConstants.BuildParameterAssembly))
                {
                    assembly = arg.Substring(StartBuildCommandConstants.BuildParameterAssembly.Length);
                    continue;
                }

                if (arg.StartsWith(StartBuildCommandConstants.BuildParameterTarget))
                {
                    target = arg.Substring(StartBuildCommandConstants.BuildParameterTarget.Length);
                    continue;
                }

                if (arg.StartsWith(StartBuildCommandConstants.BuildParameterConfiguration))
                {
                    configuration = arg.Substring(StartBuildCommandConstants.BuildParameterConfiguration.Length);
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

            var parameters = new StartBuildCommand(assembly, target, configuration, additionalParameters);
            return parameters;
        }
    }
}