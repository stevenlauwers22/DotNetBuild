using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.BuildRunnerTests
{
    public class Run_with_no_assembly
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IConfiguratorResolver> _configuratorResolver;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private Mock<ITargetRegistry> _targetRegistry;
        private Mock<ITargetExecutor> _targetExecutor;
        private UnableToLoadAssemblyException _exception;

        protected override void Arrange()
        {
            _assemblyName = null;
            _targetName = null;
            _configurationName = null;

            _assemblyLoader = new Mock<IAssemblyLoader>();
            _configuratorResolver = new Mock<IConfiguratorResolver>();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _targetRegistry = new Mock<ITargetRegistry>();
            _targetExecutor = new Mock<ITargetExecutor>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configuratorResolver.Object, _configurationRegistry.Object, _targetRegistry.Object, _targetExecutor.Object);
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