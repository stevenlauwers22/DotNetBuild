using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateRunnerCommandLinePackage : ITarget
    {
        public String Description
        {
            get { return "Create CommandLine Runner NuGet package"; }
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
            const string baseDir = @"..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine\DotNetBuild.Runner.CommandLine.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
        }
    }
}