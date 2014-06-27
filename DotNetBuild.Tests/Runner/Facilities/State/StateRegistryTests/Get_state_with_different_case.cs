using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateRegistryTests
{
    public class Get_state_with_different_case
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private Object _value;
        private Object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            var sut = new StateRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            _result = Sut.Get<Object>(_key.ToUpper());
        }

        [Fact]
        public void Returns_the_correct_value()
        {
            Assert.Equal(_value, _result);
        }
    }
}