using System;
using System.Linq;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
    }

    public class CommandDispatcher
        : ICommandDispatcher
    {
        private readonly TinyIoCContainer _container;

        public CommandDispatcher(TinyIoCContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            _container = container;
        }

        public void Dispatch(ICommand command)
        {
            var commandHandler = GetCommandHandlerFor(command);
            if (commandHandler == null)
                return;

            var commandHandlerMethod = commandHandler
                .GetType()
                .GetMethods()
                .FirstOrDefault(m => m.Name == "Handle" && m.GetParameters()[0].ParameterType == command.GetType());

            if (commandHandlerMethod != null)
                commandHandlerMethod.Invoke(commandHandler, new object[] { command });
        }

        protected object GetCommandHandlerFor(object @event)
        {
            var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(@event.GetType());
            var commandHandlers = _container.Resolve(commandHandlerType);
            return commandHandlers;
        }
    }
}