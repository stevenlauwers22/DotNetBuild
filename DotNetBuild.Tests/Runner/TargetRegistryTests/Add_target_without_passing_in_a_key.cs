﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.TargetRegistryTests
{
    public class Add_target_without_passing_in_a_key
        : TestSpecification<TargetRegistry>
    {
        private String _key;
        private ITarget _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = null;
            _value = new Mock<ITarget>().Object;
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
        public void Throws_an_ArgumentNullException_for_the_key_parameter()
        {
            Assert.NotNull(_exception);
            Assert.Equal("key", _exception.ParamName);
        }
    }
}