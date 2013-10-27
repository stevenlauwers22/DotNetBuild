﻿using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface ITargetInspector
    {
        IEnumerable<Type> CheckForCircularDependencies(ITarget target);
    }

    public class TargetInspector 
        : ITargetInspector
    {
        public IEnumerable<Type> CheckForCircularDependencies(ITarget target)
        {
            var targetTypes = new List<Type>();
            if (GetCircularDependencies(target, targetTypes))
                return targetTypes;

            return new List<Type>();
        }

        private static bool GetCircularDependencies(ITarget target, ICollection<Type> targetTypes)
        {
            var targetType = target.GetType();
            if (targetTypes.Count(t => t == targetType) == 1)
                return true;

            targetTypes.Add(targetType);
            
            if (target.DependsOn == null || !target.DependsOn.Any()) 
                return false;

            foreach (var dependentTarget in target.DependsOn)
            {
                if (GetCircularDependencies(dependentTarget, targetTypes))
                    return true;
            }

            return false;
        }
    }
}