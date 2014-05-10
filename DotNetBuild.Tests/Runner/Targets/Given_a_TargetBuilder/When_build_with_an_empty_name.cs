using System;
using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetBuilder
{
    public class When_build_with_an_empty_name : TestSpecification<TargetBuilder>
    {
        private string _name;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _name = "";
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => new TargetBuilder(new TargetRegistry(), _name, _name));
        }

        [Fact]
        public void Throws_an_ArgumentNullException()
        {
            Assert.NotNull(_exception);
            Assert.Equal("name", _exception.ParamName);
        }
    }
}