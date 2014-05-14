using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationRegistry
{
    public class When_told_to_Get_configuration_settings
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;
        private IConfigurationSettings _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            var sut = new ConfigurationRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            _result = Sut.Get(_key);
        }

        [Fact]
        public void Returns_the_correct_value()
        {
            Assert.Equal(_value, _result);
        }
    }
}