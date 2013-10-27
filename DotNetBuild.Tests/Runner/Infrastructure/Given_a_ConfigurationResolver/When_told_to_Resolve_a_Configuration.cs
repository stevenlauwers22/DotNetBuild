using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration
        : TestSpecification<ConfigurationResolver>
    {
        private string _configurationName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _configurationRegistryType;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private Mock<IConfigurationSelector> _configurationSelector;
        private Mock<IConfigurationSettings> _configurationSettings;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();

            _configurationRegistryType = typeof(IConfigurationRegistry);
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(a => a.Get<IConfigurationRegistry>()).Returns(_configurationRegistryType);

            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _typeActivator = new Mock<ITypeActivator>();
            _typeActivator.Setup(ta => ta.Activate<IConfigurationRegistry>(_configurationRegistryType)).Returns(_configurationRegistry.Object);

            _configurationSettings = new Mock<IConfigurationSettings>();
            _configurationSelector = new Mock<IConfigurationSelector>();
            _configurationSelector.Setup(cs => cs.Select(_configurationName, _configurationRegistry.Object)).Returns(_configurationSettings.Object);
        }

        protected override ConfigurationResolver CreateSubjectUnderTest()
        {
            return new ConfigurationResolver(_configurationSelector.Object, _typeActivator.Object);
        }

        protected override void Act()
        {
            _result = Sut.Resolve(_configurationName, _assemblyWrapper.Object);
        }

        [Fact]
        public void Gets_the_ConfigurationRegistry_type()
        {
            _assemblyWrapper.Verify(a => a.Get<IConfigurationRegistry>());
        }

        [Fact]
        public void Instantiates_the_ConfigurationSelector()
        {
            _typeActivator.Verify(ta => ta.Activate<IConfigurationRegistry>(_configurationRegistryType));
        }

        [Fact]
        public void Selects_the_ConfigurationSettings()
        {
            _configurationSelector.Verify(cs => cs.Select(_configurationName, _configurationRegistry.Object));
        }

        [Fact]
        public void Returns_the_ConfigurationSettings()
        {
            Assert.Equal(_configurationSettings.Object, _result);
        }
    }
}