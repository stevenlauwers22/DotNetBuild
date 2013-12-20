using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.NuGet
{
    public class CreateTasksPackage : ITarget, IWantToReadState
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