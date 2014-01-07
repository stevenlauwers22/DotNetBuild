using DotNetBuild.Runner;
using DotNetBuild.Runner.Facilities.State;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.Given_a_StateWriter
{
    public class When_told_to_Add_state
        : TestSpecification<StateWriter>
    {
        private Mock<IStateRepository> _stateRepository;
        private string _key;
        private object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();

            _stateRepository = new Mock<IStateRepository>();
        }

        protected override StateWriter CreateSubjectUnderTest()
        {
            return new StateWriter(_stateRepository.Object);
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Adds_the_state_to_the_repository()
        {
            _stateRepository.Verify(r => r.Add(_key, _value));
        }
    }
}