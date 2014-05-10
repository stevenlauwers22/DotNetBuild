using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetBuilder
{
    public class When_setting_DependsOn_target : TestSpecification<TargetBuilder>
    {
        private String _dependentTargetName;
        private ITarget _dependentTarget;
        private ITargetRegistry _targetRegistry;

        protected override void Arrange()
        {
            _targetRegistry = new TargetRegistry();
            _dependentTargetName = "dependentTarget";
            _dependentTarget = new TargetBuilder(_targetRegistry, _dependentTargetName, _dependentTargetName).GetTarget();
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return new TargetBuilder(_targetRegistry, TestData.GenerateString(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.DependsOn(_dependentTargetName);
        }

        [Fact]
        public void Adds_the_dependency()
        {
            var target = Sut.GetTarget();
            Assert.Equal(1, target.DependsOn.Count());
            Assert.True(target.DependsOn.Contains(_dependentTarget));
        }
    }
}