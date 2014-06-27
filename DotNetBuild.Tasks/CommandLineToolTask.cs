using System;
using System.Diagnostics;
using System.IO;

namespace DotNetBuild.Tasks
{
    public abstract class CommandLineToolTask
    {
        public Boolean Execute()
        {
            var toolPath = GetToolPath();
            if (!IsValidExe(toolPath))
                throw new InvalidOperationException(String.Format("{0} could not be found.", toolPath));

            var process = new Process
            {
                StartInfo =
                {
                    FileName = toolPath,
                    Arguments = GetToolArguments(),
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            var environmentVariables = Environment.GetEnvironmentVariables();
            foreach (String environmentVariableKey in environmentVariables.Keys)
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

        protected abstract String GetToolPath();
        protected abstract String GetToolArguments();

        protected static Boolean IsValidExe(String exe)
        {
            if (String.IsNullOrEmpty(exe))
                return false;

            var exePathInfo = new FileInfo(exe);
            if (!exePathInfo.Exists)
                return false;

            return true;
        }
    }
}