using System;
using DotNetBuild.Core;
using DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Add_setting_without_passing_in_a_key
        : TestSpecification<ConfigurationSettingsTest>
    {
        private String _key;
        private Object _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = null;
            _value = new Object();
        }

        protected override ConfigurationSettingsTest CreateSubjectUnderTest()
        {
            return new ConfigurationSettingsTest();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Add(_key, _value));
        }

        [Fact]
        public void Throws_an_ArgumentNullException_for_the_key_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("key", _exception.ParamName);
        }
    }
}