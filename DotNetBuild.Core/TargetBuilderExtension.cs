using System;

namespace DotNetBuild.Core
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
            var targetRegistry = GetTargetRegistry();
            return new TargetBuilder(targetRegistry, name, description);
        }

        public static void Target(this String name, ITarget target)
        {
            var targetRegistry = GetTargetRegistry();
            targetRegistry.Add(name, target);
        }

        private static Func<ITargetRegistry> _resolveTargetRegistry;
        public static void ResolveTargetRegistry(Func<ITargetRegistry> resolveTargetRegistry)
        {
            _resolveTargetRegistry = resolveTargetRegistry;
        }

        private static ITargetRegistry GetTargetRegistry()
        {
            if (_resolveTargetRegistry == null)
                throw new InvalidOperationException("DotNetBuild hasn't been configured yet");

            var targetRegistry = _resolveTargetRegistry();
            if (targetRegistry == null)
                throw new InvalidOperationException("DotNetBuild hasn't been configured yet");

            return targetRegistry;
        }
    }
}