using System;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.Targets
{
    public interface ITargetBuilder
    {
        ITarget GetTarget();
        ITargetBuilder ContinueOnError(Boolean continueOnError);
        ITargetDependencyBuilder DependsOn(String target);
        ITargetBuilder Do(Func<IConfigurationSettings, Boolean> executeFunc);
    }

    public class TargetBuilder 
        : ITargetBuilder
    {
        private readonly ITargetRegistry _targetRegistry;
        private readonly GenericTarget _target;

        public TargetBuilder(ITargetRegistry targetRegistry, String name, String description)
        {
            if (targetRegistry == null) 
                throw new ArgumentNullException("targetRegistry");

            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            _target = new GenericTarget(description);
            _targetRegistry = targetRegistry;
            _targetRegistry.Add(name, _target);
        }

        public ITarget GetTarget()
        {
            return _target;
        }

        public ITargetBuilder ContinueOnError(Boolean continueOnError)
        {
            _target.ContinueOnError = continueOnError;
            return this;
        }

        public ITargetDependencyBuilder DependsOn(String target)
        {
            _target.AddDependency(() => _targetRegistry.Get(target));
            return new TargetDependencyBuilder(this);
        }

        public ITargetBuilder Do(Func<IConfigurationSettings, Boolean> executeFunc)
        {
            _target.ExecuteFunc = executeFunc;
            return this;
        }
    }
}