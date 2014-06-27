using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.ConfigurationRegistryTests
{
    public class Add_configuration_settings_with_existing_key
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;
        private IConfigurationSettings _valueNew;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<IConfigurationSettings>().Object;
            _valueNew = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            var sut = new ConfigurationRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            Sut.Add(_key, _valueNew);
        }

        [Fact]
        public void Registry_contains_the_new_configuration_settings()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_valueNew, item.Value);
        }
    }
}