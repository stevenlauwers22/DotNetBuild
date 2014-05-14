using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetRegistry
{
    public class When_told_to_Add_target_with_existing_key
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;
        private ITarget _valueNew;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new Mock<ITarget>().Object;
            _valueNew = new Mock<ITarget>().Object;
        }

        protected override TargetRegistry CreateSubjectUnderTest()
        {
            var sut = new TargetRegistry();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            Sut.Add(_key, _valueNew);
        }

        [Fact]
        public void Registry_contains_the_new_target()
        {
            var item = Sut.Registrations.SingleOrDefault(kvp => kvp.Key == _key);
            Assert.NotNull(item);
            Assert.Equal(_key, item.Key);
            Assert.Equal(_valueNew, item.Value);
        }
    }
}