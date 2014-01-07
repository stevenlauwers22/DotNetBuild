using System;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.StartBuild.BuildRequestedToStart
{
    public class BuildRequestedToStartHandler : IDomainEventHandler<BuildRequestedToStart>
    {
        private readonly IBuildRepository _buildRepository;
        private readonly IBuildRunner _buildRunner;
        private readonly ILogger _logger;

        public BuildRequestedToStartHandler(
            IBuildRepository buildRepository, 
            IBuildRunner buildRunner,
            ILogger logger)
        {
            if (buildRepository == null) 
                throw new ArgumentNullException("buildRepository");

            if (buildRunner == null)
                throw new ArgumentNullException("buildRunner");

            if (logger == null) 
                throw new ArgumentNullException("logger");

            _buildRepository = buildRepository;
            _buildRunner = buildRunner;
            _logger = logger;
        }

        public void Handle(BuildRequestedToStart @event)
        {
            var build = _buildRepository.GetById(@event.BuildId);
            if (build == null)
                return;

            var buildRunnerParameters = new BuildRunnerParameters(build.Assembly, build.Target, build.Configuration, build.AdditionalParameters);
            _logger.Write("Build is about to start");
            _buildRunner.Run(buildRunnerParameters);
        }
    }
}