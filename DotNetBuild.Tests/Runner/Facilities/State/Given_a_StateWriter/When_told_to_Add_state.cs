using System;
using DotNetBuild.Runner.Facilities.State;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateWriter
{
    public class When_told_to_Add_state
        : TestSpecification<StateWriter>
    {
        private Mock<IStateRegistry> _stateRegistry;
        private String _key;
        private Object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();

            _stateRegistry = new Mock<IStateRegistry>();
        }

        protected override StateWriter CreateSubjectUnderTest()
        {
            return new StateWriter(_stateRegistry.Object);
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Adds_the_state_to_the_registry()
        {
            _stateRegistry.Verify(r => r.Add(_key, _value));
        }
    }
}