using System.Linq;
using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_StateRepository
{
    public class When_told_to_Add_state_to_the_store
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            return new StateRepository();
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Store_contains_the_state()
        {
            var item = Sut.Store.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}