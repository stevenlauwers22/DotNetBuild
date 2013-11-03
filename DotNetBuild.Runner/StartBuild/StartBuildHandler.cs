using System;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.StartBuild
{
    public class StartBuildHandler 
        : ICommandHandler<StartBuildCommand>
    {
        private readonly IBuildRepository _buildRepository;
        private readonly IDomainEventInitializer _domainEventInitializer;
        private readonly ILogger _logger;

        public StartBuildHandler(
            IBuildRepository buildRepository, 
            IDomainEventInitializer domainEventInitializer,
            ILogger logger)
        {
            if (buildRepository == null) 
                throw new ArgumentNullException("buildRepository");

            if (domainEventInitializer == null)
                throw new ArgumentNullException("domainEventInitializer");

            if (logger == null)
                throw new ArgumentNullException("logger");

            _buildRepository = buildRepository;
            _domainEventInitializer = domainEventInitializer;
            _logger = logger;
        }

        public void Handle(StartBuildCommand command)
        {
            var build = new Build(command.Assembly, command.Target, command.Configuration, command.AdditionalParameters);
            _buildRepository.Add(build);
            _domainEventInitializer.Initialize(build);
            _logger.Write("Build scheduled");

            build.RequestStart();
        }
    }
}