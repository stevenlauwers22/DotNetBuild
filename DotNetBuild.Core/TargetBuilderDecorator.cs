﻿using System;

namespace DotNetBuild.Core
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

        public ITargetBuilder ContinueOnError(Boolean continueOnError)
        {
            return TargetBuilder.ContinueOnError(continueOnError);
        }

        public ITargetDependencyBuilder DependsOn(String target)
        {
            return TargetBuilder.DependsOn(target);
        }

        public ITargetBuilder Do(Func<TargetExecutionContext, Boolean> executeFunc)
        {
            return TargetBuilder.Do(executeFunc);
        }
    }
}