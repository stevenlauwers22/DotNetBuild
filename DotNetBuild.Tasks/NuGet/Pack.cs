using System;
using System.Text;

namespace DotNetBuild.Tasks.NuGet
{
    public class Pack
        : CommandLineToolTask
    {
        public string NuGetExe { get; set; }
        public string NuSpecFile { get; set; }
        public string OutputDir { get; set; }
        public string Version { get; set; }

        protected override string GetToolPath()
        {
            if (IsValidExe(NuGetExe))
                return NuGetExe;

            throw new InvalidOperationException("NuGetExe could not be found.");
        }

        protected override string GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append("pack ");
            parameters.Append(NuSpecFile + " ");

            if (!string.IsNullOrEmpty(OutputDir))
                parameters.Append("-OutputDir " + OutputDir + " ");

            if (!string.IsNullOrEmpty(Version))
                parameters.Append("-Version " + Version + " ");

            return parameters.ToString();
        }
    }
}