using System;
using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationBuilder
{
    public class When_build_with_valid_arguments : TestSpecification<ConfigurationBuilder>
    {
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private String _name;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _name = TestData.GenerateString();
        }

        protected override ConfigurationBuilder CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _result = new ConfigurationBuilder(_configurationRegistry.Object, _name).GetSettings();
        }

        [Fact]
        public void Adds_the_configuration_to_the_registry()
        {
            _configurationRegistry.Verify(r => r.Add(_name, _result));
        }
    }
}