using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateRegistry
{
    public class When_told_to_Get_state_with_wrong_type
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private Object _value;
        private InvalidCastException _exception;

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
            _exception = TestHelpers.CatchException<InvalidCastException>(() => Sut.Get<String>(_key));
        }

        [Fact]
        public void Throws_an_InvalidCastException()
        {
            Assert.NotNull(_exception);
        }
    }
}