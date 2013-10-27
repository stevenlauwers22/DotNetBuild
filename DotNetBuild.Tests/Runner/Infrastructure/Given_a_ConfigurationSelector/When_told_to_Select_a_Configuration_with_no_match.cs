using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_ConfigurationSelector
{
    public class When_told_to_Select_a_Configuration_with_no_match
        : TestSpecification<ConfigurationSelector>
    {
        private string _configuration;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configuration = TestData.GenerateString();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
        }

        protected override ConfigurationSelector CreateSubjectUnderTest()
        {
            return new ConfigurationSelector();
        }

        protected override void Act()
        {
            _result = Sut.Select(_configuration, _configurationRegistry.Object);
        }

        [Fact]
        public void Gets_the_registrations()
        {
            _configurationRegistry.Verify(cr => cr.Registrations);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}