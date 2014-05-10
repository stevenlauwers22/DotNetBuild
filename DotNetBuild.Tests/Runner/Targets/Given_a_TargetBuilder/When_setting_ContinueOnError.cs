﻿using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetBuilder
{
    public class When_setting_ContinueOnError : TestSpecification<TargetBuilder>
    {
        private bool _continueOnError;

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