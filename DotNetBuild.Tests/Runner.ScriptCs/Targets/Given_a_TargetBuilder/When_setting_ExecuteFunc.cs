using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.ScriptCs.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets.Given_a_TargetBuilder
{
    public class When_setting_ExecuteFunc : TestSpecification<TargetBuilder>
    {
        private Func<IConfigurationSettings, bool> _executeFunc;

        protected override void Arrange()
        {
            _executeFunc = configurationSettings => true;
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return new TargetBuilder(TestData.GenerateString(), TestData.GenerateString());
        }

        protected override void Act()
        {
            Sut.Do(_executeFunc);
        }

        [Fact]
        public void Sets_the_ExecuteFunc()
        {
            var target = (GenericTarget) Sut.GetTarget();
            Assert.Equal(_executeFunc, target.ExecuteFunc);
        }
    }
}