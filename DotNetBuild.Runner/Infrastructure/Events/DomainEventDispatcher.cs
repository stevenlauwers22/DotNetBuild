using System;
using System.Collections;
using System.Linq;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(object @event);
    }

    public class DomainEventDispatcher
        : IDomainEventDispatcher
    {
        private readonly TinyIoCContainer _container;

        public DomainEventDispatcher(TinyIoCContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            _container = container;
        }

        public void Dispatch(object @event)
        {
            var eventHandlers = GetEventHandlersFor(@event);
            foreach (var handler in eventHandlers)
            {
                var eventHandlerMethod = handler
                    .GetType()
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters()[0].ParameterType == @event.GetType());

                if (eventHandlerMethod != null)
                    eventHandlerMethod.Invoke(handler, new[] { @event });
            }
        }

        protected IEnumerable GetEventHandlersFor(object @event)
        {
            var eventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var eventHandlers = _container.ResolveAll(eventHandlerType, true);
            return eventHandlers;
        }
    }
}