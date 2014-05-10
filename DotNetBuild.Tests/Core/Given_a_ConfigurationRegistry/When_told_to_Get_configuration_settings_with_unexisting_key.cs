using System;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry
{
    public class When_told_to_Get_configuration_settings_with_unexisting_key
        : TestSpecification<ConfigurationRegistryTest>
    {
        private String _key;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override ConfigurationRegistryTest CreateSubjectUnderTest()
        {
            return new ConfigurationRegistryTest();
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