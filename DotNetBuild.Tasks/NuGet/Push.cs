using System;
using System.Text;

namespace DotNetBuild.Tasks.NuGet
{
    public class Push
        : CommandLineToolTask
    {
        public string NuGetExe { get; set; }
        public string NuPkgFile { get; set; }
        public string ApiKey { get; set; }

        protected override string GetToolPath()
        {
            if (IsValidExe(NuGetExe))
                return NuGetExe;

            throw new InvalidOperationException("NuGetExe could not be found.");
        }

        protected override string GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append("push ");
            parameters.Append(NuPkgFile + " ");

            if (!string.IsNullOrEmpty(ApiKey))
                parameters.Append("-ApiKey " + ApiKey + " ");

            return parameters.ToString();
        }
    }
}