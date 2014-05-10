using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry
{
    public class When_told_to_Add_state_without_passing_in_a_key
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private object _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = null;
            _value = new object();
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
        public void Throws_an_ArgumentNullException_for_the_key_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("key", _exception.ParamName);
        }
    }
}