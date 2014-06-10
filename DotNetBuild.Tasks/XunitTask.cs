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
            return XunitExe;
        }

        protected override string GetToolArguments()
        {
            var parameters = new StringBuilder();
            parameters.Append(Assembly + " ");

            return parameters.ToString();
        }
    }
}