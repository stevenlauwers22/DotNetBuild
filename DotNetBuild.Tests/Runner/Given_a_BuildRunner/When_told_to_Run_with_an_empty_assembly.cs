using System;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRunner
{
    public class When_told_to_Run_with_an_empty_assembly
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITargetExecutor> _targetExecutor;
        private Mock<ILogger> _logger;
        private UnableToLoadAssemblyException _exception;

        protected override void Arrange()
        {
            _assemblyName = null;
            _targetName = null;
            _configurationName = null;

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
            _exception = TestHelpers.CatchException<UnableToLoadAssemblyException>(() => Sut.Run(_assemblyName, _targetName, _configurationName));
        }

        [Fact]
        public void Loads_the_assembly()
        {
            _assemblyLoader.Verify(al => al.Load(_assemblyName));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(null, _exception.Assembly);
        }
    }
}