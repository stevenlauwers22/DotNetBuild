using System;
using System.IO;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Reflection;
using DotNetBuild.Tests.TestAssembly;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.Given_a_AssemblyWrapper
{
    public class When_told_to_Get_one_type_with_no_filter
        : TestSpecification<AssemblyWrapper>
    {
        private Assembly _assembly;
        private Type _result;

        protected override void Arrange()
        {
            _assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DotNetBuild.Tests.TestAssembly.dll"));
        }

        protected override AssemblyWrapper CreateSubjectUnderTest()
        {
            return new AssemblyWrapper(_assembly);
        }

        protected override void Act()
        {
            _result = Sut.Get<IConfigurator>();
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_result);
            Assert.Equal(typeof(DummyConfigurator).FullName, _result.FullName);
        }
    }
}