using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Versioning
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
            const string baseDir = @"..\";
            var assemblyInfoTask = new AssemblyInfo
            {
                AssemblyInfoFiles = new[]
                {
                    Path.Combine(baseDir, @"DotNetBuild.Core\Properties\AssemblyInfo.cs"),
                    Path.Combine(baseDir, @"DotNetBuild.Runner\Properties\AssemblyInfo.cs"),
                    Path.Combine(baseDir, @"DotNetBuild.Runner.CommandLine\Properties\AssemblyInfo.cs")
                },
                AssemblyBuildNumberType="AutoIncrement",
                AssemblyBuildNumberFormat="0",
                AssemblyFileBuildNumberType="AutoIncrement",
                AssemblyFileBuildNumberFormat="0"
            };

            var result = assemblyInfoTask.Execute();
            context.FacilityProvider.Get<ILogger>().LogInfo("Building version: " +assemblyInfoTask.MaxAssemblyVersion);
            context.FacilityProvider.Get<IStateWriter>().Add("VersionNumber", assemblyInfoTask.MaxAssemblyVersion);

            return result;
        }
    }
}