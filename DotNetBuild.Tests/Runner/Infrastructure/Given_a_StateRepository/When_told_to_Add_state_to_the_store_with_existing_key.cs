using System.Linq;
using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_StateRepository
{
    public class When_told_to_Add_state_to_the_store_with_existing_key
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;
        private object _valueNew;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
            _valueNew = new object();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            var sut = new StateRepository();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            Sut.Add(_key, _valueNew);
        }

        [Fact]
        public void Store_contains_the_new_state()
        {
            var item = Sut.Store.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_valueNew, item.Value);
        }
    }
}