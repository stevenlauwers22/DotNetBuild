using System;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetResolver
{
    public class When_told_to_Resolve_a_Target_with_no_target_name
        : TestSpecification<TargetResolver>
    {
        private String _targetName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _targetName = null;
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _typeActivator = new Mock<ITypeActivator>();
        }

        protected override TargetResolver CreateSubjectUnderTest()
        {
            return new TargetResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Resolve(_targetName, _assemblyWrapper.Object));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal("targetName", _exception.ParamName);
        }
    }
}