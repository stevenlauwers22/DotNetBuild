using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.TargetRegistryTests.StaticTests
{
    public class Multiple_TargetRegistry_instances
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;
        private TargetRegistry _sut1;
        private TargetRegistry _sut2;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<ITarget>().Object;
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _sut1 = new TargetRegistry();
            _sut2 = new TargetRegistry();
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