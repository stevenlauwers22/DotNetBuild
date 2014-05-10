using System;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.Targets
{
    public abstract class TargetBuilderDecorator 
        : ITargetBuilder
    {
        protected readonly ITargetBuilder TargetBuilder;

        protected TargetBuilderDecorator(ITargetBuilder targetBuilder)
        {
            if (targetBuilder == null) 
                throw new ArgumentNullException("targetBuilder");

            TargetBuilder = targetBuilder;
        }

        public ITarget GetTarget()
        {
            return TargetBuilder.GetTarget();
        }

        public ITargetBuilder ContinueOnError(bool continueOnError)
        {
            return TargetBuilder.ContinueOnError(continueOnError);
        }

        public ITargetDependencyBuilder DependsOn(String target)
        {
            return TargetBuilder.DependsOn(target);
        }

        public ITargetBuilder Do(Func<IConfigurationSettings, bool> executeFunc)
        {
            return TargetBuilder.Do(executeFunc);
        }
    }
}