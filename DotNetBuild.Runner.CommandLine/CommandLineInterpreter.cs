using System;
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
        private readonly TinyIoCContainer _container;

        public CommandLineInterpreter(TinyIoCContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            _container = container;
        }

        public ICommand Interpret(string[] args)
        {
            if (args == null || args.Length == 0) 
                return null;

            ICommandBuilder commandBuilder;

            var commandName = args[0];
            if (!_container.TryResolve(commandName, out commandBuilder)) 
                return null;

            var command = commandBuilder.BuildFrom(args);
            return command;
        }
    }
}