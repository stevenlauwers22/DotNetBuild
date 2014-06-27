using System;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Reflection.TypeActivatorTests
{
    public class Activate_a_Type
        : TestSpecification<TypeActivator>
    {
        private Type _type;
        private IType _result;

        protected override void Arrange()
        {
            _type = typeof(DummyType);
        }

        protected override TypeActivator CreateSubjectUnderTest()
        {
            return new TypeActivator();
        }

        protected override void Act()
        {
            _result = Sut.Activate<IType>(_type);
        }

        [Fact]
        public void Creates_an_instance_of_the_Target()
        {
            Assert.NotNull(_result);
            Assert.True(_type.IsInstanceOfType(_result));
        }

        private interface IType
        {
        }

        private class DummyType : IType
        {
        }
    }
}