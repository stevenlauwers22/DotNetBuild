using System;
using System.Linq;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.ConfigurationSettingsTests
{
    public class Add_setting_with_existing_key
        : TestSpecification<ConfigurationSettings>
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

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            var sut = new ConfigurationSettings();
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