using System;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Targets
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
            var container = TinyIoCContainer.Current;
            var targetRegistry = container.Resolve<ITargetRegistry>();
            return new TargetBuilder(targetRegistry, name, description);
        }
    }
}