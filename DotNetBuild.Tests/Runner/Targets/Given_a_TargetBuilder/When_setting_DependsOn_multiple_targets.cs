using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.Targets.Given_a_TargetBuilder
{
    public class When_setting_DependsOn_multiple_targets : TestSpecification<TargetBuilder>
    {
        private String _dependentTarget1Name;
        private String _dependentTarget2Name;
        private ITarget _dependentTarget1;
        private ITarget _dependentTarget2;
        private TargetRegistry _targetRegistry;

        protected override void Arrange()
        {
            _targetRegistry = new TargetRegistry();

            _dependentTarget1Name = "dependentTarget1";
            _dependentTarget1 = new TargetBuilder(_targetRegistry, _dependentTarget1Name, _dependentTarget1Name).GetTarget();

            _dependentTarget2Name = "dependentTarget2";
            _dependentTarget2 = new TargetBuilder(_targetRegistry, _dependentTarget2Name, _dependentTarget2Name).GetTarget();
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return new TargetBuilder(_targetRegistry, TestData.GenerateString(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.DependsOn(_dependentTarget1Name)
               .And(_dependentTarget2Name);
        }

        [Fact]
        public void Adds_the_dependency()
        {
            var target = Sut.GetTarget();
            Assert.Equal(2, target.DependsOn.Count());
            Assert.True(target.DependsOn.Contains(_dependentTarget1));
            Assert.True(target.DependsOn.Contains(_dependentTarget2));
        }
    }
}