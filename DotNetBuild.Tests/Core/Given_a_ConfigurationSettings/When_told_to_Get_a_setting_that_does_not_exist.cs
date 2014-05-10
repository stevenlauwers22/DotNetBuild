using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Get_a_setting_that_does_not_exist
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private String _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            return new ConfigurationSettingsTest();
        }

        protected override void Act()
        {
            _result = Sut.Get<String>(_key);
        }

        [Fact]
        public void Returns_the_setting()
        {
            Assert.Null(_result);
        }

        private class ConfigurationSettingsTest : ConfigurationSettings
        {
        }
    }
}