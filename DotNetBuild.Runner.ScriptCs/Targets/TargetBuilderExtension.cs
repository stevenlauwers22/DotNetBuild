namespace DotNetBuild.Runner.ScriptCs.Targets
{
    public static class TargetBuilderExtension
    {
        public static ITargetBuilder Target(this string name)
        {
            var description = name;
            return name.Target(description);
        }

        public static ITargetBuilder Target(this string name, string description)
        {
            return new TargetBuilder(name, description);
        }
    }
}