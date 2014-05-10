using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using MSBuild.ExtensionPack.Framework;

namespace DotNetBuild.Build.Versioning
{
    public class UpdateVersionNumber : ITarget, IWantToWriteState, IWantToLog
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

        public Boolean Execute(IConfigurationSettings configurationSettings)
        {
            var baseDir = configurationSettings.Get<String>("baseDir");
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
            _logger.LogInfo("Building version: " +assemblyInfoTask.MaxAssemblyVersion);
            _stateWriter.Add("VersionNumber", assemblyInfoTask.MaxAssemblyVersion);

            return result;
        }

        private IStateWriter _stateWriter;
        public void Inject(IStateWriter stateWriter)
        {
            _stateWriter = stateWriter;
        }

        private ILogger _logger;
        public void Inject(ILogger logger)
        {
            _logger = logger;
        }
    }
}