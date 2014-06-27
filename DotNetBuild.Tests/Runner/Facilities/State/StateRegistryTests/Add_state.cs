using System;
using System.Linq;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateRegistryTests
{
    public class Add_state
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private Object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();
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