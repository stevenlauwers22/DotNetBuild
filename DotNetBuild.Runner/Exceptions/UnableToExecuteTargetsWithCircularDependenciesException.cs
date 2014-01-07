using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBuild.Runner.Exceptions
{
    public class UnableToExecuteTargetWithCircularDependenciesException 
        : DotNetBuildException
    {
        private readonly IEnumerable<Type> _circularDependencies;

        public UnableToExecuteTargetWithCircularDependenciesException(ICollection<Type> circularDependencies)
            : base(-17, string.Format("A circular dependency was found: {0}", GetCircularDependencyChain(circularDependencies)))
        {
            _circularDependencies = circularDependencies;
        }

        public IEnumerable<Type> CircularDependencies
        {
            get { return _circularDependencies; }
        }

        private static object GetCircularDependencyChain(IEnumerable<Type> circularDependencies)
        {
            var callTree = Environment.NewLine + circularDependencies.Aggregate(string.Empty, (current, circularDependency) => current + (" => " + circularDependency + Environment.NewLine)) + " => repeat from beginning ...";
            return callTree;
        }
    }
}