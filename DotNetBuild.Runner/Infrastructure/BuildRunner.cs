using System;
using System.IO;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface IBuildRunner
    {
        void Run(BuildRunnerParameters parameters);
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

        public void Run(BuildRunnerParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            var assemblyName = parameters.Assembly;
            if (assemblyName == null)
                throw new UnableToLoadAssemblyException(assemblyName);

            // TODO: clean up
            var assemblyFile = new FileInfo(assemblyName);
            var assemblyFiles = assemblyFile.Directory.GetFiles();
            var assemblyFilesTarget = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            if (!assemblyFilesTarget.Exists)
                assemblyFilesTarget.Create();

            foreach (var fileInfo in assemblyFiles)
            {
                var fileInfoTarget = new FileInfo(Path.Combine(assemblyFilesTarget.FullName, fileInfo.Name));
                if (fileInfoTarget.Exists)
                    continue;

                _logger.Write(string.Format("Copy {0} to {1} ", fileInfo.FullName, fileInfoTarget.FullName));
                fileInfo.CopyTo(fileInfoTarget.FullName);
            }

            var assembly = _assemblyLoader.Load(assemblyName);
            if (assembly == null)
                throw new UnableToLoadAssemblyException(assemblyName);

            var configurationSettings = _configurationResolver.Resolve(parameters.Configuration, assembly);
            var targetName = string.IsNullOrEmpty(parameters.Target) ? TargetConstants.DefaultTarget : parameters.Target;
            var target = _targetResolver.Resolve(targetName, assembly);
            if (target == null)
                throw new UnableToResolveTargetException(targetName, assemblyName);

            _logger.Write("Build started");
            _targetExecutor.Execute(target, configurationSettings);
        }
    }
}