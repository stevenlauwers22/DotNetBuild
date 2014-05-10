using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetRegistry
{
    public class When_told_to_Add_target_without_passing_in_a_value
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = null;
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            return new TargetRegistry();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Add(_key, _value));
        }

        [Fact]
        public void Throws_an_ArgumentNullException_for_the_value_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("value", _exception.ParamName);
        }
    }
}