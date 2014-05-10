using System;
using System.IO;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Tests.TestAssembly;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_AssemblyWrapper
{
    public class When_told_to_Get_one_type_with_a_filter
        : TestSpecification<AssemblyWrapper>
    {
        private Assembly _assembly;
        private String _target;
        private Type _result;

        protected override void Arrange()
        {
            _assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DotNetBuild.Tests.TestAssembly.dll"));
            _target = "Dummy1Target";
        }

        protected override AssemblyWrapper CreateSubjectUnderTest()
        {
            return new AssemblyWrapper(_assembly);
        }

        protected override void Act()
        {
            _result = Sut.Get<ITarget>(new AssemblyTypeFilter(t => String.Equals(t.Name, _target)));
        }

        [Fact]
        public void Gets_the_specified_type()
        {
            Assert.NotNull(_result);
            Assert.Equal(typeof(Dummy1Target).FullName, _result.FullName);
        }
    }
}