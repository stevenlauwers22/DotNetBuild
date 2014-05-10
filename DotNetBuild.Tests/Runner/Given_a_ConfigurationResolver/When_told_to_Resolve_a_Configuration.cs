using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration
        : TestSpecification<ConfigurationResolver>
    {
        private String _configurationName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _configurationRegistryType;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private Mock<IConfigurationSettings> _configurationSettings;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();

            _configurationRegistryType = typeof(IConfigurationRegistry);
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(a => a.Get<IConfigurationRegistry>()).Returns(_configurationRegistryType);

            _configurationSettings = new Mock<IConfigurationSettings>();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _configurationRegistry.Setup(r => r.Get(_configurationName)).Returns(_configurationSettings.Object);

            _typeActivator = new Mock<ITypeActivator>();
            _typeActivator.Setup(ta => ta.Activate<IConfigurationRegistry>(_configurationRegistryType)).Returns(_configurationRegistry.Object);
        }

        protected override ConfigurationResolver CreateSubjectUnderTest()
        {
            return new ConfigurationResolver(_typeActivator.Object);
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
            _configurationRegistry.Verify(r => r.Get(_configurationName));
        }

        [Fact]
        public void Returns_the_ConfigurationSettings()
        {
            Assert.Equal(_configurationSettings.Object, _result);
        }
    }
}