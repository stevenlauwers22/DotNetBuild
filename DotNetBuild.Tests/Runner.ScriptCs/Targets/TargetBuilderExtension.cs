namespace DotNetBuild.Tests.Runner.ScriptCs.Targets
{
    public static class TargetBuilderExtension
    {
        public static ITargetBuilder Target(this string name)
        {
            return new TargetBuilder(name);
        }
    }
}