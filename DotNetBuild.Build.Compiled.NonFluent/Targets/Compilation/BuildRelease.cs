using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets.Compilation
{
    public class BuildRelease : ITarget
    {
        public String Description
        {
            get { return "Build in release mode"; }
        }

        public Boolean ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return null; }
        }

        public Boolean Execute(TargetExecutionContext context)
        {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var msBuildTask = new MsBuildTask
            {
                Project = Path.Combine(solutionDirectory, @"DotNetBuild.sln"),
                Target = "Rebuild",
                Parameters = "Configuration=Release"
            };

            return msBuildTask.Execute();
        }
    }
}