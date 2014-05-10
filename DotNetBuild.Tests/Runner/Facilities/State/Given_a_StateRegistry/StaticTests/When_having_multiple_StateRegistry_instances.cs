using System;
using System.Linq;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry.StaticTests
{
    public class When_having_multiple_StateRegistry_instances
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private object _value;
        private StateRegistry _sut1;
        private StateRegistry _sut2;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _sut1 = new StateRegistry();
            _sut2 = new StateRegistry();
            _sut2.Add(_key, _value);
        }

        [Fact]
        public void Registry1_and_Registry2_are_the_same()
        {
            Assert.Equal(_sut1.Registrations, _sut2.Registrations);
        }

        [Fact]
        public void Registry1_contains_the_state()
        {
            var item = _sut1.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }

        [Fact]
        public void Registry2_contains_the_state()
        {
            var item = _sut2.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}