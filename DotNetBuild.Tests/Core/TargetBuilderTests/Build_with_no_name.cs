using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Core.TargetBuilderTests
{
    public class Build_with_no_name : TestSpecification<TargetBuilder>
    {
        private String _name;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _name = null;
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