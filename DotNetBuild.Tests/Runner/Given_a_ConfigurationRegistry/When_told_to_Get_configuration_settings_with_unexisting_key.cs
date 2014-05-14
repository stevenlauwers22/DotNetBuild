using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationRegistry
{
    public class When_told_to_Get_configuration_settings_with_unexisting_key
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private Object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            return new ConfigurationRegistry();
        }

        protected override void Act()
        {
            _result = Sut.Get(_key);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}