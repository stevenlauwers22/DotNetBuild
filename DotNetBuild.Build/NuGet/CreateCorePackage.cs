using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Core.Targets;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateCorePackage : ITarget, IWantToReadState
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

        public Boolean Execute(IConfigurationSettings configurationSettings)
        {
            var baseDir = configurationSettings.Get<String>("baseDir");
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild\DotNetBuild.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild"),
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