using System;
using System.Linq;
using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry
{
    public class When_told_to_Add_configuration_settings
        : TestSpecification<ConfigurationRegistryTest>
    {
        private String _key;
        private IConfigurationSettings _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistryTest CreateSubjectUnderTest()
        {
            return new ConfigurationRegistryTest();
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