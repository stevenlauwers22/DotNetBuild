using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationRegistry
{
    public class When_told_to_Add_configuration_settings_without_passing_in_a_key
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = null;
            _value = new Mock<IConfigurationSettings>().Object;
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
        public void Throws_an_ArgumentNullException_for_the_key_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("key", _exception.ParamName);
        }
    }
}