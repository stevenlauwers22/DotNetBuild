using System;
using System.Collections.Generic;
using DotNetBuild.Build.Assembly.NonFluent.Targets.NuGet;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets
{
    public class Deploy : ITarget
    {
        public String Description
        {
            get { return "Deploy to NuGet"; }
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
                    new PublishCorePackage(),
                    new PublishRunnerPackage(),
                    new PublishRunnerAssemblyPackage(),
                    new PublishRunnerScriptCsPackage(),
                    new PublishTasksPackage()
                };
            }
        }

        public Boolean Execute(TargetExecutionContext context)
        {
            return true;
        }
    }
}