using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry
{
    public class When_told_to_Get_state
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private object _value;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            var sut = new StateRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            _result = Sut.Get<object>(_key);
        }

        [Fact]
        public void Returns_the_correct_value()
        {
            Assert.Equal(_value, _result);
        }
    }
}