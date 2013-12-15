using System.Collections.Generic;
using DotNetBuild.Core;
using MSBuild.ExtensionPack.Framework;

namespace DotNetBuild.Build.Versioning
{
    public class UpdateVersionNumber : ITarget
    {
        public string Name
        {
            get { return "Update version number"; }
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
            // TODO: don't use full path + clean up assemblyinfo task
            var assemblyInfoTasks = new AssemblyInfo
            {
                AssemblyInfoFiles = new[]
                {
                    @"C:\Projects\DotNetBuild\DotNetBuild\DotNetBuild.Core\Properties\AssemblyInfo.cs",
                    @"C:\Projects\DotNetBuild\DotNetBuild\DotNetBuild.Runner\Properties\AssemblyInfo.cs",
                    @"C:\Projects\DotNetBuild\DotNetBuild\DotNetBuild.Runner.CommandLine\Properties\AssemblyInfo.cs"
                },
                AssemblyBuildNumberType="AutoIncrement",
                AssemblyBuildNumberFormat="0",
                AssemblyFileBuildNumberType="AutoIncrement",
                AssemblyFileBuildNumberFormat="0"
            };

            var result = assemblyInfoTasks.Execute();
            VersionNumber = assemblyInfoTasks.MaxAssemblyVersion;
            return result;
        }

        public static string VersionNumber { get; set; }
    }
}