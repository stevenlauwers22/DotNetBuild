using System.Linq;

namespace DotNetBuild.Runner.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        void Dispatch(ICommand command);
    }

    public class CommandDispatcher
        : ICommandDispatcher
    {
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
            var commandHandlers = TinyIoC.TinyIoCContainer.Current.Resolve(commandHandlerType);
            return commandHandlers;
        }
    }
}