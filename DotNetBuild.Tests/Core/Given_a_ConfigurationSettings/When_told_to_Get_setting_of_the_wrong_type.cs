using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Get_setting_of_the_wrong_type
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private decimal _value;
        private InvalidCastException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = TestData.GenerateDecimal();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            var configurationSettings = new ConfigurationSettings();
            configurationSettings.Add(_key, _value);

            return configurationSettings;
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<InvalidCastException>(() => Sut.Get<String>(_key));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
        }
    }
}