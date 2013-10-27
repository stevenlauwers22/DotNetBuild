using System;
using System.Reflection;

namespace DotNetBuild.Runner.Infrastructure.Events
{
    public class DefaultDomainEventConfigurator
        : IDomainEventConfiguration<DomainEvent>
    {
        private readonly IDomainEventDispatcher _eventDispatcher;

        public DefaultDomainEventConfigurator(IDomainEventDispatcher dispatcher)
        {
            _eventDispatcher = dispatcher;
        }

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof(DomainEvent); }
        }

        public DomainEvent HandleEvent
        {
            get { return @event => _eventDispatcher.Dispatch(@event); }
        }
    }
}