using System;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.TestAssembly
{
    public class DummyConfigurator : IConfigurator
    {
        public void Configure()
        {
            throw new NotImplementedException();
        }
    }
}