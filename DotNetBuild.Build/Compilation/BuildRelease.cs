using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Compilation
{
    public class BuildRelease : ITarget
    {
        public string Name
        {
            get { return "Build in release mode"; }
        }

        public bool ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return null; }
        }

        public bool Execute(IConfigurationSettings configurationSettings)
        {
            var msBuildTask = new MsBuildTask
            {
                Project = @"C:\Projects\DotNetBuild\DotNetBuild\DotNetBuild.sln",
                Target = "Rebuild",
                Parameters = "Configuration=Release"
            };

            return msBuildTask.Execute();
        }
    }
}