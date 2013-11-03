using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_BuildRunner
{
    public class When_told_to_Run_with_an_empty_assembly
        : TestSpecification<BuildRunner>
    {
        private BuildRunnerParameters _parameters;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITargetExecutor> _targetExecutor;
        private Mock<ILogger> _logger;
        private UnableToLoadAssemblyException _exception;

        protected override void Arrange()
        {
            _parameters = new BuildRunnerParameters(null, null, null, null);

            _assemblyLoader = new Mock<IAssemblyLoader>();
            _configurationResolver = new Mock<IConfigurationResolver>();
            _targetResolver = new Mock<ITargetResolver>();
            _targetExecutor = new Mock<ITargetExecutor>();
            _logger = new Mock<ILogger>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configurationResolver.Object, _targetResolver.Object, _targetExecutor.Object, _logger.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToLoadAssemblyException>(() => Sut.Run(_parameters));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(null, _exception.Assembly);
        }
    }
}