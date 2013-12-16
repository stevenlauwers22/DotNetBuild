using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_StateRepository
{
    public class When_told_to_Get_state_from_the_store
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            var sut = new StateRepository();
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