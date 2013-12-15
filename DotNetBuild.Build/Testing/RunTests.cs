using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Tasks;

namespace DotNetBuild.Build.Testing
{
    public class RunTests : ITarget
    {
        public string Name
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
            var xunitTask = new XunitTask
            {
                XunitExe = @"C:\Projects\DotNetBuild\DotNetBuild\packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe",
                Assembly = @"C:\Projects\DotNetBuild\DotNetBuild\DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll"
            };

            return xunitTask.Execute();
        }
    }
}