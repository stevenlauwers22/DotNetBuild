using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateRegistryTests
{
    public class Add_state_without_passing_in_a_value
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private Object _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = null;
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            return new StateRegistry();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Add(_key, _value));
        }

        [Fact]
        public void Throws_an_ArgumentNullException_for_the_value_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("value", _exception.ParamName);
        }
    }
}