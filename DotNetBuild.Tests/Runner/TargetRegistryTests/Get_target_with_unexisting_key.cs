﻿using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.TargetRegistryTests
{
    public class Get_target_with_unexisting_key
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private Object _result;

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