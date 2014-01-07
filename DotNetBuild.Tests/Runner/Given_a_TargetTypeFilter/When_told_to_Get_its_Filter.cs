using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetTypeFilter
{
    public class When_told_to_Get_its_Filter
        : TestSpecification<TargetTypeFilter>
    {
        private Func<Type, bool> _result;

        protected override void Arrange()
        {
        }

        protected override TargetTypeFilter CreateSubjectUnderTest()
        {
            return new TargetTypeFilter("DummyTarget");
        }

        protected override void Act()
        {
            _result = Sut.Filter;
        }

        [Fact]
        public void Returns_the_Filter_passed_in_its_constructor()
        {
            Assert.NotNull(_result);
        }
    }
}