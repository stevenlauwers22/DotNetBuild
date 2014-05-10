using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Get_a_setting
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
            return new ConfigurationSettingsTest(_key, _value);
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

        private class ConfigurationSettingsTest : ConfigurationSettings
        {
            public ConfigurationSettingsTest(String key, object value)
            {
                Add(key, value);
            }
        }
    }
}