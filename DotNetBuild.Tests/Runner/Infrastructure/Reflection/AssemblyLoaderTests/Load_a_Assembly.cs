﻿using System;
using System.IO;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.AssemblyLoaderTests
{
    public class Load_a_Assembly
        : TestSpecification<AssemblyLoader>
    {
        private String _assembly;
        private IAssemblyWrapper _result;

        protected override void Arrange()
        {
            _assembly = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DotNetBuild.Tests.TestAssembly.dll");
        }

        protected override AssemblyLoader CreateSubjectUnderTest()
        {
            return new AssemblyLoader();
        }

        protected override void Act()
        {
            _result = Sut.Load(_assembly);
        }

        [Fact]
        public void Wraps_the_assembly()
        {
            Assert.NotNull(_result);
        }
    }
}