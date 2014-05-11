using System;
using DotNetBuild.Core;
using DotNetBuild.Core.Targets;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;

namespace DotNetBuild.Runner
{
    public interface IBuildRunner
    {
        void Run(String assemblyName, String targetName, String configurationName);
        void Run(Action configure, String targetName, String configurationName);
    }

    public class BuildRunner 
        : IBuildRunner
    {
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IConfiguratorResolver _configuratorResolver;
        private readonly IConfigurationRegistry _configurationRegistry;
        private readonly ITargetRegistry _targetRegistry;
        private readonly ITargetExecutor _targetExecutor;

        public BuildRunner(
            IAssemblyLoader assemblyLoader, 
            IConfiguratorResolver configuratorResolver, 
            IConfigurationRegistry configurationRegistry,
            ITargetRegistry targetRegistry,
            ITargetExecutor targetExecutor)
        {
            if (assemblyLoader == null)
                throw new ArgumentNullException("assemblyLoader");

            if (configuratorResolver == null) 
                throw new ArgumentNullException("configuratorResolver");

            if (configurationRegistry == null) 
                throw new ArgumentNullException("configurationRegistry");

            if (targetRegistry == null) 
                throw new ArgumentNullException("targetRegistry");

            if (targetExecutor == null) 
                throw new ArgumentNullException("targetExecutor");

            _assemblyLoader = assemblyLoader;
            _configuratorResolver = configuratorResolver;
            _configurationRegistry = configurationRegistry;
            _targetRegistry = targetRegistry;
            _targetExecutor = targetExecutor;
        }

        public void Run(String assemblyName, String targetName, String configurationName)
        {
            var assembly = _assemblyLoader.Load(assemblyName);
            if (assembly == null)
                throw new UnableToLoadAssemblyException(assemblyName);

            var configurator = _configuratorResolver.Resolve(assembly);
            if (configurator == null)
                throw new UnableToResolveConfiguratorException(assemblyName);

            Run(configurator.Configure, targetName, configurationName);
        }

        public void Run(Action configure, String targetName, String configurationName)
        {
            configure();

            var target = _targetRegistry.Get(targetName);
            if (target == null)
                throw new UnableToFindTargetException(targetName);

            IConfigurationSettings configurationSettings = null;
            if (!String.IsNullOrEmpty(configurationName))
            {
                configurationSettings = _configurationRegistry.Get(configurationName);

                if (configurationSettings == null)
                    throw new UnableToFindConfigurationException(configurationName);
            }

            _targetExecutor.Execute(target, configurationSettings);
        }
    }
}