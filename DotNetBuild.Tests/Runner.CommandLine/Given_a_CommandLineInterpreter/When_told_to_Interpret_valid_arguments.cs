using DotNetBuild.Runner.CommandLine;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.CommandLine.Given_a_CommandLineInterpreter
{
    public class When_told_to_Interpret_valid_arguments
        : TestSpecification<CommandLineInterpreter>
    {
        private string[] _args;
        private TinyIoCContainer _container;
        private Mock<ICommand> _command1;
        private Mock<ICommandBuilder> _commandBuilder1;
        private Mock<ICommand> _command2;
        private Mock<ICommandBuilder> _commandBuilder2;
        private ICommand _result;

        protected override void Arrange()
        {
            _args = new[] { "cb-1", "foo", "bar" };

            _command1 = new Mock<ICommand>();
            _commandBuilder1 = new Mock<ICommandBuilder>();
            _commandBuilder1.Setup(cb => cb.BuildFrom(_args)).Returns(_command1.Object);

            _command2 = new Mock<ICommand>();
            _commandBuilder2 = new Mock<ICommandBuilder>();
            _commandBuilder2.Setup(cb => cb.BuildFrom(_args)).Returns(_command2.Object);

            _container = new TinyIoCContainer();
            _container.Register(typeof(ICommandBuilder), _commandBuilder1.Object, "cb-1");
            _container.Register(typeof(ICommandBuilder), _commandBuilder2.Object, "cb-2");
        }

        protected override CommandLineInterpreter CreateSubjectUnderTest()
        {
            return new CommandLineInterpreter(_container);
        }

        protected override void Act()
        {
            _result = Sut.Interpret(_args);
        }

        [Fact]
        public void Tells_the_CommandBuilder_to_build_the_Command()
        {
            _commandBuilder1.Verify(cb => cb.BuildFrom(_args));
        }

        [Fact]
        public void Returns_the_appropriate_Command()
        {
            Assert.Equal(_result, _command1.Object);
        }
    }
}