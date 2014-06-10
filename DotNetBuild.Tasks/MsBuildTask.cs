using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetBuild.Tasks
{
    public class MsBuildTask
        : CommandLineToolTask
    {
        public string MsBuildExe { get; set; }
        public string Project { get; set; }
        public string Target { get; set; }
        public string Parameters { get; set; }

        protected override string GetToolPath()
        {
            if (IsValidExe(MsBuildExe))
                return MsBuildExe;

            var alternativeMsBuildExes = new List<string>
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

        protected override string GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append(Project + " ");

            if (!string.IsNullOrEmpty(Target))
                parameters.Append("/t:" + Target + " ");

            if (!string.IsNullOrEmpty(Parameters))
                parameters.Append("/p:" + Parameters + " ");

            return parameters.ToString();
        }
    }
}