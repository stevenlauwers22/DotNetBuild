using System;
using System.Diagnostics;

namespace DotNetBuild.Tasks
{
    public abstract class CommandLineToolTask
    {
        public Boolean Execute()
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = GetToolPath(),
                    Arguments = GetToolArguments(),
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            var environmentVariables = Environment.GetEnvironmentVariables();
            foreach (string environmentVariableKey in environmentVariables.Keys)
            {
                if (process.StartInfo.EnvironmentVariables.ContainsKey(environmentVariableKey))
                    continue;

                process.StartInfo.EnvironmentVariables.Add(environmentVariableKey, environmentVariables[environmentVariableKey].ToString());
            }

            process.Start();
            process.WaitForExit();

            var exitCode = process.ExitCode;
            return exitCode == 0;
        }

        protected abstract string GetToolPath();
        protected abstract string GetToolArguments();
    }
}