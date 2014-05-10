using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration_with_no_configuration_registry_type
        : TestSpecification<ConfigurationResolver>
    {
        private String _configurationName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _typeActivator = new Mock<ITypeActivator>();
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
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}