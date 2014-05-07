using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetResolver
{
    public class When_told_to_Resolve_a_Target
        : TestSpecification<TargetResolver>
    {
        private string _targetName;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _targetType;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<ITarget> _target;
        private ITarget _result;

        protected override void Arrange()
        {
            _targetName = TestData.GenerateString();

            _targetType = typeof (ITarget);
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(a => a.Get<ITarget>(It.Is<TargetTypeFilter>(f => f.TargetName == _targetName))).Returns(_targetType);

            _target = new Mock<ITarget>();
            _typeActivator = new Mock<ITypeActivator>();
            _typeActivator.Setup(ta => ta.Activate<ITarget>(_targetType)).Returns(_target.Object);
        }

        protected override TargetResolver CreateSubjectUnderTest()
        {
            return new TargetResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _result = Sut.Resolve(_targetName, _assemblyWrapper.Object);
        }

        [Fact]
        public void Gets_the_Target_type()
        {
            _assemblyWrapper.Verify(a => a.Get<ITarget>(It.Is<TargetTypeFilter>(f => f.TargetName == _targetName)));
        }

        [Fact]
        public void Instantiates_the_Target()
        {
            _typeActivator.Verify(ta => ta.Activate<ITarget>(_targetType));
        }

        [Fact]
        public void Returns_the_Target()
        {
            Assert.Equal(_target.Object, _result);
        }
    }
}