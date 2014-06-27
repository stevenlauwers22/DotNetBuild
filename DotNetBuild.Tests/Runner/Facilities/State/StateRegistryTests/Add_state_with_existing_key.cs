using System;
using System.Linq;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateRegistryTests
{
    public class Add_state_with_existing_key
        : TestSpecification<StateRegistry>
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

        protected override StateRegistry CreateSubjectUnderTest()
        {
            var sut = new StateRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            Sut.Add(_key, _valueNew);
        }

        [Fact]
        public void Registry_contains_the_new_state()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_valueNew, item.Value);
        }
    }
}