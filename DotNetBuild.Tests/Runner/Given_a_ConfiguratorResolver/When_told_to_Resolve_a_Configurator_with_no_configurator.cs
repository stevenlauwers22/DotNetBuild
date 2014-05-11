using System;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfiguratorResolver
{
    public class When_told_to_Resolve_a_Configurator_with_no_configurator
        : TestSpecification<ConfiguratorResolver>
    {
        private Assembly _assembly;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _configurationType;
        private Mock<ITypeActivator> _typeActivator;
        private UnableToActivateConfiguratorException _exception;

        protected override void Arrange()
        {
            _assembly = GetType().Assembly;
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(aw => aw.Assembly).Returns(_assembly);

            _configurationType = typeof(IConfigurator);
            _assemblyWrapper.Setup(a => a.Get<IConfigurator>()).Returns(_configurationType);

            _typeActivator = new Mock<ITypeActivator>();
        }

        protected override ConfiguratorResolver CreateSubjectUnderTest()
        {
            return new ConfiguratorResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToActivateConfiguratorException>(() => Sut.Resolve(_assemblyWrapper.Object));
        }

        [Fact]
        public void Gets_the_Configurator_type()
        {
            _assemblyWrapper.Verify(a => a.Get<IConfigurator>());
        }

        [Fact]
        public void Instantiates_the_Configurator()
        {
            _typeActivator.Verify(ta => ta.Activate<IConfigurator>(_configurationType));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_configurationType, _exception.ConfiguratorType);
        }
    }
}