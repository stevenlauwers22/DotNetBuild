using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistration
{
    public class When_told_to_Get_its_Configuration
        : TestSpecification<ConfigurationRegistration>
    {
        private IConfigurationSettings _configurationSettings;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _configurationSettings = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistration CreateSubjectUnderTest()
        {
            return new ConfigurationRegistration(_configurationSettings, p => true);
        }

        protected override void Act()
        {
            _result = Sut.Configuration;
        }

        [Fact]
        public void Returns_the_Configuration_passed_in_its_constructor()
        {
            Assert.Equal(_configurationSettings, _result);
        }
    }
}