using System;

namespace DotNetBuild.Tasks
{
    public class ExecTask
        : CommandLineToolTask
    {
        public String ToolExe { get; set; }
        public String ToolArguments { get; set; }

        protected override String GetToolPath()
        {
            return ToolExe;
        }

        protected override String GetToolArguments()
        {
            return ToolArguments;
        }
    }
}