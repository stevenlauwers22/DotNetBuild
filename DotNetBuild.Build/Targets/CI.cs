using System;
using System.Collections.Generic;
using DotNetBuild.Build.Targets.Compilation;
using DotNetBuild.Build.Targets.NuGet;
using DotNetBuild.Build.Targets.Testing;
using DotNetBuild.Build.Targets.Versioning;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Targets
{
    public class CI : ITarget
    {
        public String Description
        {
            get { return "Continuous integration target"; }
        }

        public Boolean ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get
            {
                return new List<ITarget>
                {
                    new UpdateVersionNumber(),
                    new BuildRelease(),
                    new RunTests(),
                    new CreateCorePackage(),
                    new CreateRunnerPackage(),
                    new CreateRunnerCommandLinePackage(),
                    new CreateRunnerScriptCsPackage(),
                    new CreateTasksPackage()
                };
            }
        }

        public Boolean Execute(TargetExecutionContext context)
        {
            return true;
        }
    }
}