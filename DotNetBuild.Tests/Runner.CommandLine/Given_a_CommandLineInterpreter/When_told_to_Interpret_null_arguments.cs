using DotNetBuild.Runner.CommandLine;
using DotNetBuild.Runner.Infrastructure.Commands;
using DotNetBuild.Runner.Infrastructure.TinyIoC;
using Xunit;

namespace DotNetBuild.Tests.Runner.CommandLine.Given_a_CommandLineInterpreter
{
    public class When_told_to_Interpret_null_arguments
        : TestSpecification<CommandLineInterpreter>
    {
        private string[] _args;
        private TinyIoCContainer _container;
        private ICommand _result;

        protected override void Arrange()
        {
            _args = null;
            _container = new TinyIoCContainer();
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
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}