using System;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfiguratorResolver
{
    public class When_told_to_Resolve_a_Configurator_with_no_assembly_wrapper
        : TestSpecification<ConfiguratorResolver>
    {
        private IAssemblyWrapper _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _assemblyWrapper = null;
            _typeActivator = new Mock<ITypeActivator>();
        }

        protected override ConfiguratorResolver CreateSubjectUnderTest()
        {
            return new ConfiguratorResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Resolve(_assemblyWrapper));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal("assembly", _exception.ParamName);
        }
    }
}