﻿using System;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets
{
    public interface ITargetBuilder
    {
        ITarget GetTarget();
        ITargetBuilder ContinueOnError(bool continueOnError);
        ITargetDependencyBuilder DependsOn(string target);
        ITargetBuilder Do(Func<IConfigurationSettings, bool> executeFunc);
    }

    public class TargetBuilder 
        : ITargetBuilder
    {
        private readonly GenericTarget _target;
        
        public TargetBuilder(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException("name");

            _target = new GenericTarget(name);
            TargetRepository.Add(name, _target);
        }

        public ITarget GetTarget()
        {
            return _target;
        }

        public ITargetBuilder ContinueOnError(bool continueOnError)
        {
            _target.ContinueOnError = continueOnError;
            return this;
        }

        public ITargetDependencyBuilder DependsOn(string target)
        {
            _target.AddDependency(() => TargetRepository.Get(target));
            return new TargetDependencyBuilder(this);
        }

        public ITargetBuilder Do(Func<IConfigurationSettings, bool> executeFunc)
        {
            _target.ExecuteFunc = executeFunc;
            return this;
        }
    }
}