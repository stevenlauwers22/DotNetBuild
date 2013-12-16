using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository
{
    public class When_told_to_Get_a_Build_from_the_store
        : TestSpecification<BuildRepository>
    {
        private Build _build;
        private Build _result;

        protected override void Arrange()
        {
            _build = new Build(null, null, null, null);
        }

        protected override BuildRepository CreateSubjectUnderTest()
        {
            var sut = new BuildRepository();
            sut.Add(_build);

            return sut;
        }

        protected override void Act()
        {
            _result = Sut.GetById(_build.Id);
        }

        [Fact]
        public void Returns_the_correct_value()
        {
            Assert.Equal(_build, _result);
        }
    }
}