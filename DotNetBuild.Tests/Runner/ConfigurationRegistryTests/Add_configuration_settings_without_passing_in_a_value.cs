using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.ConfigurationRegistryTests
{
    public class Add_configuration_settings_without_passing_in_a_value
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = null;
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            return new ConfigurationRegistry();
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