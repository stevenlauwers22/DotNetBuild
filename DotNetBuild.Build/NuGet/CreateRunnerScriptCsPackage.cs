using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateRunnerScriptCsPackage : ITarget, IWantToReadState
    {
        public string Name
        {
            get { return "Create ScriptCs Runner NuGet package"; }
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
            var baseDir = configurationSettings.Get<string>("baseDir");
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.ScriptCs\DotNetBuild.Runner.ScriptCs.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.ScriptCs"),
                Version = _stateReader.Get<string>("VersionNumber")
            };

            return nugetPackTask.Execute();
        }

        private IStateReader _stateReader;
        public void Inject(IStateReader stateReader)
        {
            _stateReader = stateReader;
        }
    }
}