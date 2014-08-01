using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.Compiled.NonFluent.Targets.NuGet
{
    public class CreateTasksPackage : ITarget
    {
        public String Description
        {
            get { return "Create Tasks NuGet package"; }
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
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuSpecFile = Path.Combine(solutionDirectory, @"packagesForNuGet\DotNetBuild.Tasks.nuspec"),
                OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
        }
    }
}