using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.Events;

namespace DotNetBuild.Runner.StartBuild
{
    public class StartBuildHandler 
        : ICommandHandler<StartBuildCommand>
    {
        private readonly IBuildRepository _buildRepository;
        private readonly IDomainEventInitializer _domainEventInitializer;

        public StartBuildHandler(
            IBuildRepository buildRepository, 
            IDomainEventInitializer domainEventInitializer)
        {
            _buildRepository = buildRepository;
            _domainEventInitializer = domainEventInitializer;
        }

        public void Handle(StartBuildCommand command)
        {
            var build = new Build(command.Assembly, command.Target, command.Configuration, command.AdditionalParameters);
            _buildRepository.Add(build);
            _domainEventInitializer.Initialize(build);
            
            build.RequestStart();
        }
    }
}