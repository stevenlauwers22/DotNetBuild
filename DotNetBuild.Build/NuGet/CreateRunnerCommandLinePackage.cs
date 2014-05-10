using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateRunnerCommandLinePackage : ITarget, IWantToReadState
    {
        public String Description
        {
            get { return "Create CommandLine Runner NuGet package"; }
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
            var baseDir = configurationSettings.Get<String>("baseDir");
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine\DotNetBuild.Runner.CommandLine.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine"),
                Version = _stateReader.Get<String>("VersionNumber")
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