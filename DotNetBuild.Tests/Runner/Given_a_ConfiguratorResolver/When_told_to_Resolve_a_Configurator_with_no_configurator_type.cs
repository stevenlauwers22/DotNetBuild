using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfiguratorResolver
{
    public class When_told_to_Resolve_a_Configurator_with_no_configurator_type
        : TestSpecification<ConfiguratorResolver>
    {
        private Assembly _assembly;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private UnableToResolveConfiguratorException _exception;

        protected override void Arrange()
        {
            _assembly = GetType().Assembly;
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(aw => aw.Assembly).Returns(_assembly);

            _typeActivator = new Mock<ITypeActivator>();
        }

        protected override ConfiguratorResolver CreateSubjectUnderTest()
        {
            return new ConfiguratorResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToResolveConfiguratorException>(() => Sut.Resolve(_assemblyWrapper.Object));
        }

        [Fact]
        public void Gets_the_Configurator_type()
        {
            _assemblyWrapper.Verify(a => a.Get<IConfigurator>());
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_assembly.FullName, _exception.Assembly);
        }
    }
}