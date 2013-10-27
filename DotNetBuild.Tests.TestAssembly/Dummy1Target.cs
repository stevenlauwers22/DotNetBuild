using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.TestAssembly
{
    public class Dummy1Target : ITarget
    {
        public string Name
        {
            get { return "Dummy target 1"; }
        }

        public bool ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return new List<ITarget> { new Dummy2Target() }; }
        }

        public bool Execute(IConfigurationSettings configurationSettings)
        {
            Debug.WriteLine("{0} - executing", Name);
            return true;
        }
    }
}