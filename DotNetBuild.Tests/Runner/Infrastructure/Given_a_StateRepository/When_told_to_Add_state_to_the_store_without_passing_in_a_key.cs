﻿using System;
using DotNetBuild.Runner.Infrastructure;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_StateRepository
{
    public class When_told_to_Add_state_to_the_store_without_passing_in_a_key
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _key = null;
            _value = new object();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            return new StateRepository();
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