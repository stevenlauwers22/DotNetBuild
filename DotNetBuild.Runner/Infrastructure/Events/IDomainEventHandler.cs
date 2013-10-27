namespace DotNetBuild.Runner.Infrastructure.Events
{
    public interface IDomainEventHandler<in T>
    {
        void Handle(T @event);
    }
}