using System;
using System.Text;

namespace DotNetBuild.Tasks.NuGet
{
    public class Push
        : CommandLineToolTask
    {
        public String NuGetExe { get; set; }
        public String NuPkgFile { get; set; }
        public String ApiKey { get; set; }

        protected override String GetToolPath()
        {
            if (IsValidExe(NuGetExe))
                return NuGetExe;

            throw new InvalidOperationException("NuGetExe could not be found.");
        }

        protected override String GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append("push ");
            parameters.Append(NuPkgFile + " ");

            if (!String.IsNullOrEmpty(ApiKey))
                parameters.Append("-ApiKey " + ApiKey + " ");

            return parameters.ToString();
        }
    }
}