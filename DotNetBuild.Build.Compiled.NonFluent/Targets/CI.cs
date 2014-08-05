using System;
using System.Collections.Generic;
using DotNetBuild.Build.Assembly.NonFluent.Targets.Compilation;
using DotNetBuild.Build.Assembly.NonFluent.Targets.NuGet;
using DotNetBuild.Build.Assembly.NonFluent.Targets.Testing;
using DotNetBuild.Build.Assembly.NonFluent.Targets.Versioning;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets
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
                    new CreateRunnerAssemblyPackage(),
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