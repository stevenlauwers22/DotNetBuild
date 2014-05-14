using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationRegistry.StaticTests
{
    public class When_having_multiple_ConfigurationRegistry_instances
        : TestSpecification<ConfigurationRegistry>
    {
        private String _key;
        private IConfigurationSettings _value;
        private ConfigurationRegistry _sut1;
        private ConfigurationRegistry _sut2;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<IConfigurationSettings>().Object;
        }

        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _sut1 = new ConfigurationRegistry();
            _sut2 = new ConfigurationRegistry();
            _sut2.Add(_key, _value);
        }

        [Fact]
        public void Registry1_and_Registry2_are_the_same()
        {
            Assert.Equal(_sut1.Registrations, _sut2.Registrations);
        }

        [Fact]
        public void Registry1_contains_the_target()
        {
            var item = _sut1.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }

        [Fact]
        public void Registry2_contains_the_target()
        {
            var item = _sut2.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}