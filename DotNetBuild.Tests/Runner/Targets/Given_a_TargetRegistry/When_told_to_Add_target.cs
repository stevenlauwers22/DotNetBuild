using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Targets;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetRegistry
{
    public class When_told_to_Add_target
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<ITarget>().Object;
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            return new TargetRegistry();
        }

        protected override void Act()
        {
            Sut.Add(_key, _value);
        }

        [Fact]
        public void Registry_contains_the_target()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_value, item.Value);
        }
    }
}