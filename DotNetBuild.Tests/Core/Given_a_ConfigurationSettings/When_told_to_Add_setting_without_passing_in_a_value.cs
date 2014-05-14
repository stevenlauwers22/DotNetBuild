using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Add_setting_without_passing_in_a_value
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private Object _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = null;
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            return new ConfigurationSettings();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Add(_key, _value));
        }

        [Fact]
        public void Throws_an_ArgumentNullException_for_the_value_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("value", _exception.ParamName);
        }
    }
}