using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.TestAssembly
{
    public class Dummy2Target : ITarget
    {
        public string Description
        {
            get { return "Dummy target 2"; }
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
            Debug.WriteLine("{0} - executing", Description);
            return true;
        }
    }
}