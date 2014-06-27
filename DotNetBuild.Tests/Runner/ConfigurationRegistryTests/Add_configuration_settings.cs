using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.ConfigurationRegistryTests
{
    public class Add_configuration_settings
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            return new ConfigurationRegistry();
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Registry_contains_the_configuration_settings()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}