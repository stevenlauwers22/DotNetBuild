using System;
using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetExecutor
{
    public class When_told_to_Execute_a_Target_with_a_safe_exception
        : TestSpecification<TargetExecutor>
    {
        private Mock<ITarget> _target;
        private Mock<ITarget> _dependentTarget;
        private Mock<ITargetInspector> _targetInspector;
        private Mock<ILogger> _logger;
        private UnableToExecuteTargetException _exception;

        protected override void Arrange()
        {
            _dependentTarget = new Mock<ITarget>();
            _dependentTarget.Setup(t => t.ContinueOnError).Returns(false);
            _dependentTarget.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(false);

            _target = new Mock<ITarget>();
            _target.Setup(t => t.DependsOn).Returns(new List<ITarget> { _dependentTarget.Object });
            _target.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);

            _targetInspector = new Mock<ITargetInspector>();
            _targetInspector.Setup(ti => ti.CheckForCircularDependencies(_target.Object)).Returns(new List<Type>());

            _logger = new Mock<ILogger>();
        }

        protected override TargetExecutor CreateSubjectUnderTest()
        {
            return new TargetExecutor(_targetInspector.Object, _logger.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToExecuteTargetException>(() => Sut.Execute(_target.Object, null));
        }

        [Fact]
        public void Executes_the_dependent_Target()
        {
            _dependentTarget.Verify(t => t.Execute(It.IsAny<IConfigurationSettings>()));
        }

        [Fact]
        public void Does_not_execute_the_Target()
        {
            _target.Verify(t => t.Execute(It.IsAny<IConfigurationSettings>()), Times.Never);
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_dependentTarget.Object.GetType(), _exception.TargetType);
        }
    }
}