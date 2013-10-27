using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Events;

namespace DotNetBuild.Runner.StartBuild.BuildRequestedToStart
{
    public class BuildRequestedToStartHandler : IDomainEventHandler<BuildRequestedToStart>
    {
        private readonly IBuildRepository _buildRepository;
        private readonly IBuildRunner _buildRunner;

        public BuildRequestedToStartHandler(IBuildRepository buildRepository, IBuildRunner buildRunner)
        {
            _buildRepository = buildRepository;
            _buildRunner = buildRunner;
        }

        public void Handle(BuildRequestedToStart @event)
        {
            var build = _buildRepository.GetById(@event.BuildId);
            if (build == null)
                return;

            var buildRunnerParameters = new BuildRunnerParameters(build.Assembly, build.Target, build.Configuration, build.AdditionalParameters);
            _buildRunner.Run(buildRunnerParameters);
        }
    }
}