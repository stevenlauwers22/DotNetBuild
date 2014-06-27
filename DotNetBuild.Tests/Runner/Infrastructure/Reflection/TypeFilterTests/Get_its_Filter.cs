using System;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.TypeFilterTests
{
    public class Get_its_Filter
        : TestSpecification<TypeFilter>
    {
        private Func<Type, Boolean> _filter;
        private Func<Type, Boolean> _result;

        protected override void Arrange()
        {
            _filter = t => true;
        }

        protected override TypeFilter CreateSubjectUnderTest()
        {
            return new TypeFilter(_filter);
        }

        protected override void Act()
        {
            _result = Sut.Filter;
        }

        [Fact]
        public void Returns_the_Filter_passed_in_its_constructor()
        {
            Assert.Equal(_filter, _result);
        }
    }
}