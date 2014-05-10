using System;
using System.Linq;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry
{
    public class When_told_to_Add_state_with_existing_key
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private object _value;
        private object _valueNew;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
            _valueNew = new object();
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