using System.Linq;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_StateRepository.StaticTests
{
    public class When_having_multiple_StateRepository_instances
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;
        private StateRepository _sut1;
        private StateRepository _sut2;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }
        protected override StateRepository CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _sut1 = new StateRepository();
            _sut2 = new StateRepository();
            _sut2.Add(_key, _value);
        }

        [Fact]
        public void Store1_and_Store2_are_the_same()
        {
            Assert.Equal(_sut1.Store, _sut2.Store);
        }

        [Fact]
        public void Store1_contains_the_state()
        {
            var item = _sut1.Store.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }

        [Fact]
        public void Store2_contains_the_state()
        {
            var item = _sut2.Store.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}