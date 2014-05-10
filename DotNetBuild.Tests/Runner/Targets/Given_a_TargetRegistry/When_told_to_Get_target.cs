using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Targets;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetRegistry
{
    public class When_told_to_Get_target
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;
        private ITarget _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<ITarget>().Object;
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            var sut = new TargetRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            _result = Sut.Get(_key);
        }

        [Fact]
        public void Returns_the_correct_value()
        {
            Assert.Equal(_value, _result);
        }
    }
}