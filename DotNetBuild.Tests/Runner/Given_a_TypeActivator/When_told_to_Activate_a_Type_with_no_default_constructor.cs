using System;
using System.Diagnostics;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TypeActivator
{
    public class When_told_to_Activate_a_Type_with_no_default_constructor
        : TestSpecification<TypeActivator>
    {
        private Type _type;
        private UnableToActivateTypeWithNoDefaultConstructorException _exception;

        protected override void Arrange()
        {
            _type = typeof (DummyType);
        }

        protected override TypeActivator CreateSubjectUnderTest()
        {
            return new TypeActivator();
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToActivateTypeWithNoDefaultConstructorException>(() => Sut.Activate<IType>(_type));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_type, _exception.Type);
        }

        private interface IType
        {
        }

        private class DummyType : IType
        {
            public DummyType(String name)
            {
                Debug.WriteLine(name);
            }
        }
    }
}