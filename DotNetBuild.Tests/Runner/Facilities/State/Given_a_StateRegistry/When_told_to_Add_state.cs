using System;
using System.Linq;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry
{
    public class When_told_to_Add_state
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            return new StateRegistry();
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Registry_contains_the_state()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}