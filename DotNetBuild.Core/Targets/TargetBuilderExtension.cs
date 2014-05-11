using System;

namespace DotNetBuild.Core.Targets
{
    public static class TargetBuilderExtension
    {
        public static ITargetBuilder Target(this String name)
        {
            var description = name;
            return name.Target(description);
        }

        public static ITargetBuilder Target(this String name, String description)
        {
            // TODO: resolve targetregistry
            var targetRegistry = new TargetRegistry();
            return new TargetBuilder(targetRegistry, name, description);
        }

        public static void Target(this String name, ITarget target)
        {
            // TODO: resolve targetregistry
            var targetRegistry = new TargetRegistry();
            targetRegistry.Add(name, target);
        }
    }
}