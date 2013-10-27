﻿using System;
using System.IO;
using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_AssemblyLoader
{
    public class When_told_to_Load_a_Assembly
        : TestSpecification<AssemblyLoader>
    {
        private string _assembly;
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
            Assert.Equal(_result.Assembly.Location, _assembly);
        }
    }
}