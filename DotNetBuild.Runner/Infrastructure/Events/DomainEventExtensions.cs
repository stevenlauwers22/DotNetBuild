namespace DotNetBuild.Runner.Infrastructure.Events
{
    public static class DomainEventExtensions
    {
        public static void InvokeIfNotNull(this DomainEvent eventHandler, object @event)
        {
            if (eventHandler == null)
                return;

            eventHandler.Invoke(@event);
        }
    }
}