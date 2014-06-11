using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.TestAssembly
{
    public class Dummy2Target : ITarget
    {
        public String Description
        {
            get { return "Dummy target 2"; }
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
            Debug.WriteLine("{0} - executing", Description);
            return true;
        }
    }
}