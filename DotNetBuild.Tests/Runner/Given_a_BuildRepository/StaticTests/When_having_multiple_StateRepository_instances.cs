using System.Linq;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository.StaticTests
{
    public class When_having_multiple_BuildRepository_instances
        : TestSpecification<BuildRepository>
    {
        private Build _build;
        private BuildRepository _sut1;
        private BuildRepository _sut2;

        protected override void Arrange()
        {
            _build = new Build(null, null, null, null);
        }
        protected override BuildRepository CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _sut1 = new BuildRepository();
            _sut2 = new BuildRepository();
            _sut2.Add(_build);
        }

        [Fact]
        public void Store1_and_Store2_are_the_same()
        {
            Assert.Equal(_sut1.Store, _sut2.Store);
        }

        [Fact]
        public void Store1_contains_the_state()
        {
            var item = _sut1.Store.SingleOrDefault(b => b == _build);
            Assert.NotNull(item);
            Assert.Equal(_build, item);
        }

        [Fact]
        public void Store2_contains_the_state()
        {
            var item = _sut2.Store.SingleOrDefault(b => b == _build);
            Assert.NotNull(item);
            Assert.Equal(_build, item);
        }
    }
}