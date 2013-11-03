using System;
using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_TargetExecutor
{
    public class When_told_to_Execute_without_a_Target
        : TestSpecification<TargetExecutor>
    {
        private Mock<ITarget> _target;
        private Mock<ITarget> _dependentTarget1;
        private Mock<ITarget> _dependentTarget1A;
        private Mock<ITarget> _dependentTarget1B;
        private Mock<ITarget> _dependentTarget2;
        private Mock<ITarget> _dependentTarget2A;
        private Mock<ITarget> _dependentTarget2B;
        private Mock<ITarget> _dependentTarget3;
        private Mock<IConfigurationSettings> _configurationSettings;
        private Mock<ITargetInspector> _targetInspector;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _target = new Mock<ITarget>();
            _target.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget1 = new Mock<ITarget>();
            _dependentTarget1.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget1A = new Mock<ITarget>();
            _dependentTarget1A.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget1B = new Mock<ITarget>();
            _dependentTarget1B.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget2 = new Mock<ITarget>();
            _dependentTarget2.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget2A = new Mock<ITarget>();
            _dependentTarget2A.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget2B = new Mock<ITarget>();
            _dependentTarget2B.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            _dependentTarget3 = new Mock<ITarget>();
            _dependentTarget3.Setup(t => t.Execute(It.IsAny<IConfigurationSettings>())).Returns(true);
            
            var dependentTargets = new List<ITarget>
            {
                _dependentTarget1.Object, 
                _dependentTarget2.Object
            };

            var depdendentTargets1 = new List<ITarget>
            {
                _dependentTarget1A.Object,
                _dependentTarget1B.Object
            };

            var depdendentTargets2 = new List<ITarget>
            {
                _dependentTarget2A.Object,
                _dependentTarget2B.Object
            };

            _target.Setup(t => t.DependsOn).Returns(dependentTargets);
            _dependentTarget1.Setup(t => t.DependsOn).Returns(depdendentTargets1);
            _dependentTarget2.Setup(t => t.DependsOn).Returns(depdendentTargets2);

            _configurationSettings = new Mock<IConfigurationSettings>();

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
            Sut.Execute(_target.Object, _configurationSettings.Object);
        }

        [Fact]
        public void Checks_for_circular_dependencies()
        {
            _targetInspector.Verify(ti => ti.CheckForCircularDependencies(_target.Object));
        }

        [Fact]
        public void Executes_all_dependent_Targets()
        {
            _dependentTarget1A.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget1B.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget1.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget2A.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget2B.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget2.Verify(t => t.Execute(_configurationSettings.Object));
            _dependentTarget3.Verify(t => t.Execute(It.IsAny<IConfigurationSettings>()), Times.Never());
        }

        [Fact]
        public void Executes_the_Target()
        {
            _target.Verify(t => t.Execute(_configurationSettings.Object));
        }
    }
}