using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using MSBuild.ExtensionPack.Framework;

namespace DotNetBuild.Build.Versioning
{
    public class UpdateVersionNumber : ITarget, IWantToWriteState
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
            var assemblyInfoTask = new AssemblyInfo
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

            var result = assemblyInfoTask.Execute();
            _stateWriter.Add("VersionNumber", assemblyInfoTask.MaxAssemblyVersion);

            return result;
        }

        private IStateWriter _stateWriter;
        public void Inject(IStateWriter stateWriter)
        {
            _stateWriter = stateWriter;
        }
    }
}