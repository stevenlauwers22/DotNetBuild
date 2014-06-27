using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Core.TargetBuilderTests
{
    public class Set_ContinueOnError : TestSpecification<TargetBuilder>
    {
        private Boolean _continueOnError;

        protected override void Arrange()
        {
            _continueOnError = TestData.GenerateBoolean();
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return new TargetBuilder(new TargetRegistry(), TestData.GenerateString(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.ContinueOnError(_continueOnError);
        }

        [Fact]
        public void Sets_the_ContinueOnError()
        {
            var target = Sut.GetTarget();
            Assert.Equal(_continueOnError, target.ContinueOnError);
        }
    }
}