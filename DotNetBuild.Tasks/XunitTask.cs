using System;
using System.Text;

namespace DotNetBuild.Tasks
{
    public class XunitTask
        : CommandLineToolTask
    {
        public String XunitExe { get; set; }
        public String Assembly { get; set; }

        protected override String GetToolPath()
        {
            return XunitExe;
        }

        protected override String GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append(Assembly + " ");

            return parameters.ToString();
        }
    }
}