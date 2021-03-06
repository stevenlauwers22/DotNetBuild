﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Core.TargetBuilderTests
{
    public class Set_ExecuteFunc : TestSpecification<TargetBuilder>
    {
        private Func<TargetExecutionContext, Boolean> _executeFunc;

        protected override void Arrange()
        {
            _executeFunc = configurationSettings => true;
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return new TargetBuilder(new TargetRegistry(), TestData.GenerateString(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.Do(_executeFunc);
        }

        [Fact]
        public void Sets_the_ExecuteFunc()
        {
            var target = (TargetBuilder.GenericTarget) Sut.GetTarget();
            Assert.Equal(_executeFunc, target.ExecuteFunc);
        }
    }
}