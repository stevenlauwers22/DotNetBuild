using System;
using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetRegistry
{
    public class When_told_to_Get_target_with_unexisting_key
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            return new TargetRegistry();
        }

        protected override void Act()
        {
            _result = Sut.Get(_key);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}