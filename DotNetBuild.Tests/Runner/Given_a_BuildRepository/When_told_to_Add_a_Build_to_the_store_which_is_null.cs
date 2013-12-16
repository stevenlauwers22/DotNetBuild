using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository
{
    public class When_told_to_Add_a_Build_to_the_store_which_is_null
        : TestSpecification<BuildRepository>
    {
        private Build _build;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _build = null;
        }

        protected override BuildRepository CreateSubjectUnderTest()
        {
            return new BuildRepository();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Add(_build));
        }

        [Fact]
        public void Throws_an_ArgumentNullException_for_the_build_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("build", _exception.ParamName);
        }
    }
}