using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;
using DotNetBuild.Tests.TestAssembly;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.Given_a_AssemblyWrapper
{
    public class When_told_to_Get_one_type_with_no_filter_and_multiple_matches
        : TestSpecification<AssemblyWrapper>
    {
        private Assembly _assembly;
        private UnableToDetermineCorrectImplementationException _exception;

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
            _exception = TestHelpers.CatchException<UnableToDetermineCorrectImplementationException>(() => Sut.Get<ITarget>());
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(typeof(ITarget).FullName, _exception.Type.FullName);
            Assert.Equal(2, _exception.TypeImplementations.Count());
            Assert.True(_exception.TypeImplementations.Any(t => t.FullName == typeof(Dummy1Target).FullName));
            Assert.True(_exception.TypeImplementations.Any(t => t.FullName == typeof(Dummy2Target).FullName));
        }
    }
}