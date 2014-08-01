using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.ConfigurationRegistryTests
{
    public class Get_configuration_settings_without_passing_in_a_key
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private Object _result;

        protected override void Arrange()
        {
            _key = null;
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