﻿using System;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.TypeActivatorTests
{
    public class Activate_a_Type_with_no_Type
        : TestSpecification<TypeActivator>
    {
        private Type _type;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _type = null;
        }

        protected override TypeActivator CreateSubjectUnderTest()
        {
            return new TypeActivator();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Activate<IType>(_type));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal("type", _exception.ParamName);
        }

        private interface IType
        {
        }
    }
}