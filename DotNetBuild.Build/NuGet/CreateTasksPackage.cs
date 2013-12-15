using System.Collections.Generic;
using DotNetBuild.Build.Versioning;
using DotNetBuild.Core;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateTasksPackage : ITarget
    {
        public string Name
        {
            get { return "Create Tasks NuGet package"; }
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
            var nugetPackTask = new Pack
            {
                NuGetExe = @"C:\Projects\DotNetBuild\DotNetBuild\packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe",
                NuSpecFile = @"C:\Projects\DotNetBuild\DotNetBuild\packagesForNuGet\DotNetBuild.Tasks\DotNetBuild.Tasks.nuspec",
                OutputDir = @"C:\Projects\DotNetBuild\DotNetBuild\packagesForNuGet\DotNetBuild.Tasks",
                Version = UpdateVersionNumber.VersionNumber
            };

            return nugetPackTask.Execute();
        }
    }
}