using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration_with_no_configuration_registry
        : TestSpecification<ConfigurationResolver>
    {
        private string _configurationName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _configurationRegistryType;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<IConfigurationSelector> _configurationSelector;
        private UnableToActivateConfigurationRegistryException _exception;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();

            _configurationRegistryType = typeof(IConfigurationRegistry);
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(a => a.Get<IConfigurationRegistry>()).Returns(_configurationRegistryType);

            _typeActivator = new Mock<ITypeActivator>();
            _configurationSelector = new Mock<IConfigurationSelector>();
        }

        protected override ConfigurationResolver CreateSubjectUnderTest()
        {
            return new ConfigurationResolver(_configurationSelector.Object, _typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToActivateConfigurationRegistryException>(() => Sut.Resolve(_configurationName, _assemblyWrapper.Object));
        }

        [Fact]
        public void Gets_the_ConfigurationRegistry_type()
        {
            _assemblyWrapper.Verify(a => a.Get<IConfigurationRegistry>());
        }

        [Fact]
        public void Instantiates_the_ConfigurationRegistry()
        {
            _typeActivator.Verify(ta => ta.Activate<IConfigurationRegistry>(_configurationRegistryType));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_configurationRegistryType, _exception.ConfigurationRegistryType);
        }
    }
}