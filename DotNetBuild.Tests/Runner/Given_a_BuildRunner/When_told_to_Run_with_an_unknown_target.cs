using System;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRunner
{
    public class When_told_to_Run_with_an_unknown_target
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IAssemblyWrapper> _assembly;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITargetExecutor> _targetExecutor;
        private UnableToResolveTargetException _exception;

        protected override void Arrange()
        {
            _assemblyName = TestData.GenerateString();
            _targetName = TestData.GenerateString();
            _configurationName = null;

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_assemblyName)).Returns(_assembly.Object);

            _configurationResolver = new Mock<IConfigurationResolver>();
            _targetResolver = new Mock<ITargetResolver>();
            _targetExecutor = new Mock<ITargetExecutor>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configurationResolver.Object, _targetResolver.Object, _targetExecutor.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<UnableToResolveTargetException>(() => Sut.Run(_assemblyName, _targetName, _configurationName));
        }

        [Fact]
        public void Loads_the_assembly()
        {
            _assemblyLoader.Verify(al => al.Load(_assemblyName));
        }

        [Fact]
        public void Resolves_the_target()
        {
            _targetResolver.Verify(tr => tr.Resolve(_targetName, _assembly.Object));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_targetName, _exception.Target);
            Assert.Equal(_assemblyName, _exception.Assembly);
        }
    }
}