using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Get_setting
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private String _value;
        private String _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = TestData.GenerateString();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            var configurationSettings = new ConfigurationSettingsTest();
            configurationSettings.Add(_key, _value);

            return configurationSettings;
        }

        protected override void Act()
        {
            _result = Sut.Get<String>(_key);
        }

        [Fact]
        public void Returns_the_setting()
        {
            Assert.Equal(_value, _result);
        }
    }
}