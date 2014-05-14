using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBuild.Core
{
    public interface ITargetBuilder
    {
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

        public class GenericTarget
            : ITarget
        {
            private readonly String _description;
            private readonly IList<Func<ITarget>> _dependsOn;

            public GenericTarget(String description)
            {
                _description = description;
                _dependsOn = new List<Func<ITarget>>();
            }

            public String Description
            {
                get { return _description; }
            }

            public Boolean ContinueOnError
            {
                get;
                set;
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return _dependsOn.Select(t => t()).ToList(); }
            }

            public Func<IConfigurationSettings, Boolean> ExecuteFunc
            {
                get;
                set;
            }

            public void AddDependency(Func<ITarget> target)
            {
                _dependsOn.Add(target);
            }

            public Boolean Execute(IConfigurationSettings configurationSettings)
            {
                if (ExecuteFunc == null)
                    return true;

                return ExecuteFunc(configurationSettings);
            }
        }
    }
}