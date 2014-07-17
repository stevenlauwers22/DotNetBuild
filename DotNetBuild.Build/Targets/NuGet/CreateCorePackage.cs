using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.Targets.NuGet
{
    public class CreateCorePackage : ITarget
    {
        public String Description
        {
            get { return "Create Core NuGet package"; }
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
            const String baseDir = @"..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Core.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
        }
    }
}