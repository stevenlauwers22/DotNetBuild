using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TypeActivator
{
    public class When_told_to_Activate_a_Type
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