using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationSelector
{
    public class When_told_to_Select_a_Configuration_with_multiple_matches
        : TestSpecification<ConfigurationSelector>
    {
        private string _configuration;
        private Mock<IConfigurationSettings> _configurationSettingsTest1;
        private Mock<IConfigurationSettings> _configurationSettingsTest2;
        private Mock<IConfigurationSettings> _configurationSettingsAcceptance;
        private Mock<IConfigurationSettings> _configurationSettingsProduction;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private UnableToDetermineCorrectConfigurationSettingsException _exception;

        protected override void Arrange()
        {
            _configuration = "Test";
            _configurationSettingsTest1 = new Mock<IConfigurationSettings>();
            _configurationSettingsTest2 = new Mock<IConfigurationSettings>();
            _configurationSettingsAcceptance = new Mock<IConfigurationSettings>();
            _configurationSettingsProduction = new Mock<IConfigurationSettings>();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _configurationRegistry.Setup(cr => cr.Registrations).Returns(new[]
            {
                new ConfigurationRegistration(_configurationSettingsTest1.Object, configuration => configuration == "Test"),
                new ConfigurationRegistration(_configurationSettingsTest2.Object, configuration => configuration == "Test"),
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
            _exception = TestHelpers.CatchException<UnableToDetermineCorrectConfigurationSettingsException>(() => Sut.Select(_configuration, _configurationRegistry.Object));
        }

        [Fact]
        public void Gets_the_registrations()
        {
            _configurationRegistry.Verify(cr => cr.Registrations);
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(2, _exception.MatchingConfigurationSettings.Count());
            Assert.True(_exception.MatchingConfigurationSettings.Contains(_configurationSettingsTest1.Object));
            Assert.True(_exception.MatchingConfigurationSettings.Contains(_configurationSettingsTest2.Object));
        }
    }
}