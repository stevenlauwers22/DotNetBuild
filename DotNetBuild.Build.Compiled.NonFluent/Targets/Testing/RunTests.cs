using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Compiled.NonFluent.Targets.Testing
{
    public class RunTests : ITarget
    {
        public String Description
        {
            get { return "Run tests"; }
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
            var xunitTask = new XunitTask
            {
                XunitExe = Path.Combine(solutionDirectory, @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"),
                Assembly = Path.Combine(solutionDirectory, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
            };

            return xunitTask.Execute();
        }
    }
}