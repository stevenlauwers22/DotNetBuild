using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry
{
    public class When_told_to_Add_configuration_settings_without_passing_in_a_value
        : TestSpecification<ConfigurationRegistryTest>
    {
        private String _key;
        private IConfigurationSettings _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = null;
        }

        protected override ConfigurationRegistryTest CreateSubjectUnderTest()
        {
            return new ConfigurationRegistryTest();
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