using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Get_a_setting_of_the_wrong_type
        : TestSpecification<ConfigurationSettings>
    {
        private string _key;
        private decimal _value;
        private InvalidCastException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = TestData.GenerateDecimal();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            return new ConfigurationSettingsTest(_key, _value);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<InvalidCastException>(() => Sut.Get<string>(_key));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
        }

        private class ConfigurationSettingsTest : ConfigurationSettings
        {
            public ConfigurationSettingsTest(string key, object value)
            {
                Add(key, value);
            }
        }
    }
}