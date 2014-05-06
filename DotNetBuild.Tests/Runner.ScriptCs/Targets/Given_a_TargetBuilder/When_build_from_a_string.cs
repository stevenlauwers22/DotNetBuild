using Xunit;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets.Given_a_TargetBuilder
{
    public class When_build_from_a_string
    {
        [Fact]
        public void Builds_a_Target_and_fills_in_the_appropriate_name()
        {
            const string targetName = "dummy";
            var targetBuilder = targetName.Target();
            Assert.NotNull(targetBuilder);

            var target = targetBuilder.GetTarget();
            Assert.NotNull(target);
            Assert.Equal(targetName, target.Name);
        }
    }
}