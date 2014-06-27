using System;
using DotNetBuild.Core;
using Xunit;

namespace DotNetBuild.Tests.Core.ConfigurationSettingsTests
{
    public class Get_setting_with_unexisting_key_default_value
        : TestSpecification<ConfigurationSettings>
    {
        private String _key;
        private int _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override ConfigurationSettings CreateSubjectUnderTest()
        {
            return new ConfigurationSettings();
        }

        protected override void Act()
        {
            _result = Sut.Get<int>(_key);
        }

        [Fact]
        public void Returns_the_setting()
        {
            Assert.Equal(0, _result);
        }
    }
}