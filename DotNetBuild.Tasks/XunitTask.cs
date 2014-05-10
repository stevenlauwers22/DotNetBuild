using System;
using System.IO;
using System.Text;

namespace DotNetBuild.Tasks
{
    public class XunitTask
        : CommandLineToolTask
    {
        public string XunitExe { get; set; }
        public string Assembly { get; set; }

        protected override string GetToolPath()
        {
            if (IsValidExe(XunitExe))
                return XunitExe;

            throw new InvalidOperationException("XunitExe could not be found.");
        }

        protected override string GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append(Assembly + " ");

            return parameters.ToString();
        }

        private static Boolean IsValidExe(string exe)
        {
            if (string.IsNullOrEmpty(exe))
                return false;

            var exePathInfo = new FileInfo(exe);
            if (!exePathInfo.Exists)
                return false;

            return true;
        }
    }
}