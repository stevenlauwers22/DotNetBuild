using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner
{
    public interface IBuildRunner
    {
        void Run(String assemblyName, String targetName, String configurationName);
        void Run(ITarget target, IConfigurationSettings configurationSettings);
    }

    public class BuildRunner 
        : IBuildRunner
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IConfigurationResolver _configurationResolver;
        private readonly ITargetResolver _targetResolver;
        private readonly ITargetExecutor _targetExecutor;
        private readonly ILogger _logger;

        public BuildRunner(
            IAssemblyLoader assemblyLoader, 
            IConfigurationResolver configurationResolver,
            ITargetResolver targetResolver, 
            ITargetExecutor targetExecutor,
            ILogger logger)
        {
            if (assemblyLoader == null)
                throw new ArgumentNullException("assemblyLoader");

            if (configurationResolver == null)
                throw new ArgumentNullException("configurationResolver");

            if (targetResolver == null) 
                throw new ArgumentNullException("targetResolver");

            if (targetExecutor == null) 
                throw new ArgumentNullException("targetExecutor");

            if (logger == null) 
                throw new ArgumentNullException("logger");

            _assemblyLoader = assemblyLoader;
            _configurationResolver = configurationResolver;
            _targetResolver = targetResolver;
            _targetExecutor = targetExecutor;
            _logger = logger;
        }

        public void Run(String assemblyName, String targetName, String configurationName)
        {
            var assembly = _assemblyLoader.Load(assemblyName);
            if (assembly == null)
                throw new UnableToLoadAssemblyException(assemblyName);

            var targetNameOrDefault = String.IsNullOrEmpty(targetName) ? TargetConstants.DefaultTarget : targetName;
            var target = _targetResolver.Resolve(targetNameOrDefault, assembly);
            if (target == null)
                throw new UnableToResolveTargetException(targetNameOrDefault, assemblyName);

            var configurationSettings = _configurationResolver.Resolve(configurationName, assembly);
            _logger.Write("Build started");
            _targetExecutor.Execute(target, configurationSettings);
        }

        public void Run(ITarget target, IConfigurationSettings configurationSettings)
        {
            _logger.Write("Build started");
            _targetExecutor.Execute(target, configurationSettings);
        }
    }
}