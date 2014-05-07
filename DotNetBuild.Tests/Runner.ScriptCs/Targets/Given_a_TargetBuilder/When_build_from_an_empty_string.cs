using System;
using DotNetBuild.Runner.ScriptCs.Targets;
using Xunit;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets.Given_a_TargetBuilder
{
    public class When_build_from_an_empty_string
    {
        [Fact]
        public void Throws_an_ArgumentNullException()
        {
            const string targetName = "";
            var exception = TestHelpers.CatchException<ArgumentNullException>(() => targetName.Target());
            Assert.NotNull(exception);
            Assert.Equal("name", exception.ParamName);
        }
    }
}