﻿using System;
using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_AssemblyTypeFilter
{
    public class When_told_to_Get_its_Filter
        : TestSpecification<AssemblyTypeFilter>
    {
        private Func<Type, bool> _filter;
        private Func<Type, bool> _result;

        protected override void Arrange()
        {
            _filter = t => true;
        }

        protected override AssemblyTypeFilter CreateSubjectUnderTest()
        {
            return new AssemblyTypeFilter(_filter);
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