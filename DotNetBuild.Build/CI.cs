using System;
using System.Collections.Generic;
using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class CI : ITarget
    {
        public String Description
        {
            get { return "Continuous integration target"; }
        }

        public Boolean ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get
            {
                return new List<ITarget>
                {
                    new Versioning.UpdateVersionNumber(),
                    new Compilation.BuildRelease(),
                    new Testing.RunTests(),
                    new NuGet.CreateCorePackage(),
                    new NuGet.CreateRunnerCommandLinePackage(),
                    new NuGet.CreateRunnerScriptCsPackage(),
                    new NuGet.CreateTasksPackage()
                };
            }
        }

        public Boolean Execute(IConfigurationSettings configurationSettings)
        {
            return true;
        }
    }
}