namespace DotNetBuild.Tasks
{
    public class ExecTask
        : CommandLineToolTask
    {
        public string ToolExe { get; set; }
        public string ToolArguments { get; set; }

        protected override string GetToolPath()
        {
            return ToolExe;
        }

        protected override string GetToolArguments()
        {
            return ToolArguments;
        }
    }
}