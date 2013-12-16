using System.Linq;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository
{
    public class When_told_to_Add_a_Build_to_the_store
        : TestSpecification<BuildRepository>
    {
        private Build _build;

        protected override void Arrange()
        {
            _build = new Build(null, null, null, null);
        }

        protected override BuildRepository CreateSubjectUnderTest()
        {
            return new BuildRepository();
        }

        protected override void Act()
        {
            Sut.Add(_build);
        }

        [Fact]
        public void Store_contains_the_Build()
        {
            var item = Sut.Store.SingleOrDefault(b => b == _build);
            Assert.NotNull(item);
            Assert.Equal(_build, item);
        }
    }
}