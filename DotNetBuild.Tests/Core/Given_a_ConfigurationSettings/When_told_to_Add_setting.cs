using System;
using System.Linq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Add_setting
        : TestSpecification<ConfigurationSettingsTest>
    {
        private String _key;
        private Object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();
        }

        protected override ConfigurationSettingsTest CreateSubjectUnderTest()
        {
            return new ConfigurationSettingsTest();
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Registry_contains_the_setting()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}