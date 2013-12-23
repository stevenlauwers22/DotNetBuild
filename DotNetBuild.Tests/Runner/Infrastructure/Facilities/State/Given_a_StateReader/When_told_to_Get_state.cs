using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Facilities.State;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.State.Given_a_StateReader
{
    public class When_told_to_Get_state
        : TestSpecification<StateReader>
    {
        private Mock<IStateRepository> _stateRepository;
        private string _key;
        private object _value;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();

            _stateRepository = new Mock<IStateRepository>();
            _stateRepository.Setup(r => r.Get<object>(_key)).Returns(_value);
        }

        protected override StateReader CreateSubjectUnderTest()
        {
            return new StateReader(_stateRepository.Object);
        }

        protected override void Act()
        {
            _result = Sut.Get<object>(_key);
        }

        [Fact]
        public void Gets_the_state_from_repository()
        {
            _stateRepository.Verify(r => r.Get<object>(_key));
        }

        [Fact]
        public void Returns_the_correct_state()
        {
            Assert.Equal(_value, _result);
        }
    }
}