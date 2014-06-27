using System;
using System.IO;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Reflection;
using DotNetBuild.Tests.TestAssembly;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.AssemblyWrapperTests
{
    public class Get_one_type_with_a_filter
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
            _result = Sut.Get<ITarget>(new DotNetBuild.Runner.Infrastructure.Reflection.TypeFilter(t => String.Equals(t.Name, _target)));
        }

        [Fact]
        public void Gets_the_specified_type()
        {
            Assert.NotNull(_result);
            Assert.Equal(typeof(Dummy1Target).FullName, _result.FullName);
        }
    }
}