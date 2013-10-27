using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_ConfigurationSelector
{
    public class When_told_to_Select_a_Configuration
        : TestSpecification<ConfigurationSelector>
    {
        private string _configuration;
        private Mock<IConfigurationSettings> _configurationSettingsTest;
        private Mock<IConfigurationSettings> _configurationSettingsAcceptance;
        private Mock<IConfigurationSettings> _configurationSettingsProduction;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configuration = "Test";
            _configurationSettingsTest = new Mock<IConfigurationSettings>();
            _configurationSettingsAcceptance = new Mock<IConfigurationSettings>();
            _configurationSettingsProduction = new Mock<IConfigurationSettings>();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _configurationRegistry.Setup(cr => cr.Registrations).Returns(new[]
            {
                new ConfigurationRegistration(_configurationSettingsTest.Object, configuration => configuration == "Test"),
                new ConfigurationRegistration(_configurationSettingsAcceptance.Object, configuration => configuration == "Acceptance"),
                new ConfigurationRegistration(_configurationSettingsProduction.Object, configuration => configuration == "Production")
            });
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
        public void Returns_the_appropriate_configuration_settings()
        {
            Assert.Equal(_configurationSettingsTest.Object, _result);
        }
    }
}