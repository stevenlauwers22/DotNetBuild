using System;
using DotNetBuild.Runner.Facilities.State;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateReaderTests
{
    public class Get_state
        : TestSpecification<StateReader>
    {
        private Mock<IStateRegistry> _stateRegistry;
        private String _key;
        private Object _value;
        private Object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Object();

            _stateRegistry = new Mock<IStateRegistry>();
            _stateRegistry.Setup(r => r.Get<Object>(_key)).Returns(_value);
        }

        protected override StateReader CreateSubjectUnderTest()
        {
            return new StateReader(_stateRegistry.Object);
        }

        protected override void Act()
        {
            _result = Sut.Get<Object>(_key);
        }

        [Fact]
        public void Gets_the_state_from_the_registry()
        {
            _stateRegistry.Verify(r => r.Get<Object>(_key));
        }

        [Fact]
        public void Returns_the_correct_state()
        {
            Assert.Equal(_value, _result);
        }
    }
}