using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Exceptions;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface ITargetExecutor
    {
        void Execute(ITarget target, IConfigurationSettings configurationSettings);
    }

    public class TargetExecutor 
        : ITargetExecutor
    {
        private readonly ITargetInspector _targetInspector;

        public TargetExecutor(ITargetInspector targetInspector)
        {
            if (targetInspector == null) 
                throw new ArgumentNullException("targetInspector");

            _targetInspector = targetInspector;
        }

        public void Execute(ITarget target, IConfigurationSettings configurationSettings)
        {
            if (target == null) 
                throw new ArgumentNullException("target");

            var circularDependencies = _targetInspector.CheckForCircularDependencies(target).ToList();
            if (circularDependencies != null && circularDependencies.Any())
                throw new UnableToExecuteTargetWithCircularDependenciesException(circularDependencies);

            if (target.DependsOn != null && target.DependsOn.Any())
            {
                foreach (var dependentTarget in target.DependsOn)
                {   
                    Execute(dependentTarget, configurationSettings);
                }
            }

            try
            {
                var success = target.Execute(configurationSettings);
                if (!success)
                    throw new UnableToExecuteTargetException(target.GetType());
            }
            catch (UnableToExecuteTargetException)
            {
                if (!target.ContinueOnError)
                    throw;
            }
            catch (Exception exception)
            {
                if (!target.ContinueOnError)
                    throw new UnableToExecuteTargetException(target.GetType(), exception);
            }
        }
    }
}