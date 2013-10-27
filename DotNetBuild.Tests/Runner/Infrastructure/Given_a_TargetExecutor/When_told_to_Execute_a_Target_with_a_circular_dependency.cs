using System;
using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_TargetExecutor
{
    public class When_told_to_Execute_a_Target_with_a_circular_dependency
        : TestSpecification<TargetExecutor>
    {
        private Mock<ITarget> _target;
        private Mock<ITargetInspector> _targetInspector;
        private List<Type> _circularDependencies;
        private UnableToExecuteTargetWithCircularDependenciesException _exception;

        protected override void Arrange()
        {
            _target = new Mock<ITarget>();

            _circularDependencies = new List<Type> { typeof(ITarget) };
            _targetInspector = new Mock<ITargetInspector>();
            _targetInspector.Setup(ti => ti.CheckForCircularDependencies(_target.Object)).Returns(_circularDependencies);
        }

        protected override TargetExecutor CreateSubjectUnderTest()
        {
            return new TargetExecutor(_targetInspector.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToExecuteTargetWithCircularDependenciesException>(() => Sut.Execute(_target.Object, null));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_circularDependencies, _exception.CircularDependencies);
        }
    }
}