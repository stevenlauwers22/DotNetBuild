﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;

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

        public BuildRunner(
            IAssemblyLoader assemblyLoader, 
            IConfigurationResolver configurationResolver,
            ITargetResolver targetResolver, 
            ITargetExecutor targetExecutor)
        {
            if (assemblyLoader == null)
                throw new ArgumentNullException("assemblyLoader");

            if (configurationResolver == null)
                throw new ArgumentNullException("configurationResolver");

            if (targetResolver == null) 
                throw new ArgumentNullException("targetResolver");

            if (targetExecutor == null) 
                throw new ArgumentNullException("targetExecutor");

            _assemblyLoader = assemblyLoader;
            _configurationResolver = configurationResolver;
            _targetResolver = targetResolver;
            _targetExecutor = targetExecutor;
        }

        public void Run(String assemblyName, String targetName, String configurationName)
        {
            var assembly = _assemblyLoader.Load(assemblyName);
            if (assembly == null)
                throw new UnableToLoadAssemblyException(assemblyName);

            var target = _targetResolver.Resolve(targetName, assembly);
            if (target == null)
                throw new UnableToResolveTargetException(targetName, assemblyName);

            var configurationSettings = _configurationResolver.Resolve(configurationName, assembly);
            _targetExecutor.Execute(target, configurationSettings);
        }

        public void Run(ITarget target, IConfigurationSettings configurationSettings)
        {
            _targetExecutor.Execute(target, configurationSettings);
        }
    }
}