using System;
using System.Collections.Generic;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Testing
{
    public class RunTests : ITarget
    {
        public String Description
        {
            get { return "Run tests"; }
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
            var xunitTask = new XunitTask
            {
                XunitExe = Path.Combine(baseDir, @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"),
                Assembly = Path.Combine(baseDir, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
            };

            return xunitTask.Execute();
        }
    }
}