using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_StateRepository
{
    public class When_told_to_Get_state_from_the_store_with_unexisting_key
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            return new StateRepository();
        }

        protected override void Act()
        {
            _result = Sut.Get<object>(_key);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}