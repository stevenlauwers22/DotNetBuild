using System;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.ScriptCs.Targets
{
    public abstract class TargetBuilderDecorator 
        : ITargetBuilder
    {
        protected readonly ITargetBuilder _targetBuilder;

        protected TargetBuilderDecorator(ITargetBuilder targetBuilder)
        {
            if (targetBuilder == null) 
                throw new ArgumentNullException("targetBuilder");

            _targetBuilder = targetBuilder;
        }

        public ITarget GetTarget()
        {
            return _targetBuilder.GetTarget();
        }

        public ITargetBuilder ContinueOnError(bool continueOnError)
        {
            return _targetBuilder.ContinueOnError(continueOnError);
        }

        public ITargetDependencyBuilder DependsOn(string target)
        {
            return _targetBuilder.DependsOn(target);
        }

        public ITargetBuilder Do(Func<IConfigurationSettings, bool> executeFunc)
        {
            return _targetBuilder.Do(executeFunc);
        }
    }
}