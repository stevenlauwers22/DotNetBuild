using System;
using System.Linq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class When_told_to_Add_setting_with_existing_key
        : TestSpecification<ConfigurationSettingsTest>
    {
        private String _key;
        private Object _value;
        private Object _valueNew;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();
            _valueNew = new Object();
        }

        protected override ConfigurationSettingsTest CreateSubjectUnderTest()
        {
            var sut = new ConfigurationSettingsTest();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            Sut.Add(_key, _valueNew);
        }

        [Fact]
        public void Registry_contains_the_new_setting()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_valueNew, item.Value);
        }
    }
}