using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.TinyIoC;

namespace DotNetBuild.Runner.CommandLine
{
    public interface ICommandLineInterpreter
    {
        ICommand Interpret(string[] args);
    }

    public class CommandLineInterpreter : ICommandLineInterpreter
    {
        public ICommand Interpret(string[] args)
        {
            var container = TinyIoCContainer.Current;
            if (args != null && args.Length > 0)
            {
                var commandName = args[0];

                ICommandBuilder commandBuilder;
                if (container.TryResolve(commandName, out commandBuilder))
                {
                    var command = commandBuilder.BuildFrom(args);
                    return command;
                }
            }

            return null;
        }
    }
}