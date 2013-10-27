using System.Collections;
using System.Linq;

namespace DotNetBuild.Runner.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(object @event);
    }

    public class DomainEventDispatcher
        : IDomainEventDispatcher
    {
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
            var eventHandlers = TinyIoC.TinyIoCContainer.Current.ResolveAll(eventHandlerType, true);
            return eventHandlers;
        }
    }
}