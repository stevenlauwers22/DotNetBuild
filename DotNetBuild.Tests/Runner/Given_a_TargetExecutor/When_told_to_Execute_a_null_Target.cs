using System;
using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Core.Targets;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetExecutor
{
    public class When_told_to_Execute_a_null_Target
        : TestSpecification<TargetExecutor>
    {
        private ITarget _target;
        private Mock<ITargetInspector> _targetInspector;
        private Mock<ILogger> _logger;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _target = null;

            _targetInspector = new Mock<ITargetInspector>();
            _targetInspector.Setup(ti => ti.CheckForCircularDependencies(_target)).Returns(new List<Type>());

            _logger = new Mock<ILogger>();
        }

        protected override TargetExecutor CreateSubjectUnderTest()
        {
            return new TargetExecutor(_targetInspector.Object, _logger.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Execute(_target, null));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal("target", _exception.ParamName);
        }
    }
}