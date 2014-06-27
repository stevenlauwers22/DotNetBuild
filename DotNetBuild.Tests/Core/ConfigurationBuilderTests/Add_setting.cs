using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Core.ConfigurationBuilderTests
{
    public class Add_setting : TestSpecification<ConfigurationBuilder>
    {
        private String _key;
        private string _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = TestData.GenerateString();
        }

        protected override ConfigurationBuilder CreateSubjectUnderTest()
        {
            return new ConfigurationBuilder(new ConfigurationRegistry(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.AddSetting(_key, _value);
        }

        [Fact]
        public void Adds_the_setting()
        {
            var configurationSettings = Sut.GetSettings();
            Assert.Equal(configurationSettings.Get<String>(_key), _value);
        }
    }
}