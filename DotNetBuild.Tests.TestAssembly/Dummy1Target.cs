using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.TestAssembly
{
    public class Dummy1Target : ITarget
    {
        public String Description
        {
            get { return "Dummy target 1"; }
        }

        public Boolean ContinueOnError
        {
            get { return false; }
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return new List<ITarget> { new Dummy2Target() }; }
        }

        public Boolean Execute(IConfigurationSettings configurationSettings)
        {
            Debug.WriteLine("{0} - executing", Description);
            return true;
        }
    }
}