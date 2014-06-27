using System;
using System.Text;

namespace DotNetBuild.Tasks.NuGet
{
    public class Pack
        : CommandLineToolTask
    {
        public String NuGetExe { get; set; }
        public String NuSpecFile { get; set; }
        public String OutputDir { get; set; }
        public String Version { get; set; }

        protected override String GetToolPath()
        {
            if (IsValidExe(NuGetExe))
                return NuGetExe;

            throw new InvalidOperationException("NuGetExe could not be found.");
        }

        protected override String GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append("pack ");
            parameters.Append(NuSpecFile + " ");

            if (!String.IsNullOrEmpty(OutputDir))
                parameters.Append("-OutputDir " + OutputDir + " ");

            if (!String.IsNullOrEmpty(Version))
                parameters.Append("-Version " + Version + " ");

            return parameters.ToString();
        }
    }
}