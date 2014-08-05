using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets.NuGet
{
    public class PublishCorePackage : ITarget
    {
        public String Description
        {
            get { return "Publish Core NuGet package"; }
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
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Core.{0}.nupkg", context.ConfigurationSettings.Get<String>("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
            };

            return nugetPackTask.Execute();
        }
    }
}