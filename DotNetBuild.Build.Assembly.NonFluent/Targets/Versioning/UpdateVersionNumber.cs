using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Assembly.NonFluent.Targets.Versioning
{
    public class UpdateVersionNumber : ITarget
    {
        public String Description
        {
            get { return "Update version number"; }
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
            const String assemblyMajorVersion = "1";
            const String assemblyMinorVersion = "1";
            const String assemblyBuildNumber = "0";
            var assemblyInfoTask = new AssemblyInfo
            {
                AssemblyInfoFiles = new[]
                {
                    Path.Combine(solutionDirectory, @"DotNetBuild.Core\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Runner\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Runner.Assembly\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Runner.ScriptCs\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Tasks\Properties\AssemblyInfo.cs")
                },
                AssemblyInformationalVersion = String.Format("{0}.{1}.{2}", assemblyMajorVersion, assemblyMinorVersion, assemblyBuildNumber),
                UpdateAssemblyInformationalVersion = true,
                AssemblyMajorVersion = assemblyMajorVersion,
                AssemblyMinorVersion = assemblyMinorVersion,
                AssemblyBuildNumber = assemblyBuildNumber,
                AssemblyRevision = "0",
                AssemblyFileMajorVersion = assemblyMajorVersion,
                AssemblyFileMinorVersion = assemblyMinorVersion,
                AssemblyFileBuildNumber = assemblyBuildNumber,
                AssemblyFileRevision = "0"
            };

            var result = assemblyInfoTask.Execute();
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly version: " + assemblyInfoTask.MaxAssemblyVersion);
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly informational version: " + assemblyInfoTask.AssemblyInformationalVersion);
            context.FacilityProvider.Get<IStateWriter>().Add("VersionNumber", assemblyInfoTask.AssemblyInformationalVersion);

            return result;
        }
    }
}