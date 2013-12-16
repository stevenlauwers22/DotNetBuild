using System;
using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;

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
        private readonly ILogger _logger;

        public TargetExecutor(
            ITargetInspector targetInspector, 
            ILogger logger)
        {
            if (targetInspector == null) 
                throw new ArgumentNullException("targetInspector");
            
            if (logger == null) 
                throw new ArgumentNullException("logger");

            _targetInspector = targetInspector;
            _logger = logger;
        }

        public void Execute(ITarget target, IConfigurationSettings configurationSettings)
        {
            if (target == null) 
                throw new ArgumentNullException("target");

            _logger.Write("Checking target for circular dependencies");

            var circularDependencies = _targetInspector.CheckForCircularDependencies(target).ToList();
            if (circularDependencies.Any())
            {
                _logger.Write("A circular dependencies was found, cannot execute target");
                throw new UnableToExecuteTargetWithCircularDependenciesException(circularDependencies);
            }

            _logger.Write("No circular dependencies found");
            
            ExecuteTarget(target, configurationSettings);
        }

        private void ExecuteTarget(ITarget target, IConfigurationSettings configurationSettings)
        {
            try
            {
                _logger.WriteBlockStart(target.Name);
                _logger.Write("Target executing");
                
                if (target.DependsOn != null && target.DependsOn.Any())
                {
                    foreach (var dependentTarget in target.DependsOn)
                    {
                        ExecuteTarget(dependentTarget, configurationSettings);
                    }
                }

                var success = target.Execute(configurationSettings);
                if (!success)
                    throw new UnableToExecuteTargetException(target.GetType());

                _logger.Write("Target executed with success");
            }
            catch (UnableToExecuteTargetException exception)
            {
                _logger.WriteError("Target executed with an error", exception);
                _logger.Write("Continue on error: " + target.ContinueOnError.ToString().ToLower());

                if (!target.ContinueOnError)
                {
                    _logger.Write("Script will stop");
                    throw;
                }

                _logger.Write("Script will continue");
            }
            catch (Exception exception)
            {
                _logger.WriteError("Target executed with an unexpected error", exception);
                _logger.Write("Continue on error: " + target.ContinueOnError.ToString().ToLower());

                if (!target.ContinueOnError)
                {
                    _logger.Write("Script will stop");
                    throw new UnableToExecuteTargetException(target.GetType(), exception);
                }

                _logger.Write("Script will continue");
            }
            finally
            {
                _logger.WriteBlockEnd(target.Name);
            }
        }
    }
}