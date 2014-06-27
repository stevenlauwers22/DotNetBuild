using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetBuild.Tasks
{
    public class MsBuildTask
        : CommandLineToolTask
    {
        public String MsBuildExe { get; set; }
        public String Project { get; set; }
        public String Target { get; set; }
        public String Parameters { get; set; }

        protected override String GetToolPath()
        {
            if (IsValidExe(MsBuildExe))
                return MsBuildExe;

            var alternativeMsBuildExes = new List<String>
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.Net\Framework\v4.0.30319\MSBuild.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.Net\Framework\v3.5\MSBuild.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.Net\Framework\v2.0.50727\MSBuild.exe")
            };

            foreach (var alternativeMsBuildExe in alternativeMsBuildExes)
            {
                if (IsValidExe(alternativeMsBuildExe))
                    return alternativeMsBuildExe;
            }

            throw new InvalidOperationException("MsBuildExe could not be found.");
        }

        protected override String GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append(Project + " ");

            if (!String.IsNullOrEmpty(Target))
                parameters.Append("/t:" + Target + " ");

            if (!String.IsNullOrEmpty(Parameters))
                parameters.Append("/p:" + Parameters + " ");

            return parameters.ToString();
        }
    }
}