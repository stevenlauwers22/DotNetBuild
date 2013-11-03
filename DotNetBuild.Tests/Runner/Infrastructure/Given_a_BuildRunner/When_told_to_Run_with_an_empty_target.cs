using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_BuildRunner
{
    public class When_told_to_Run_with_an_empty_target
        : TestSpecification<BuildRunner>
    {
        private BuildRunnerParameters _parameters;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IAssemblyWrapper> _assembly;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITarget> _target;
        private Mock<ITargetExecutor> _targetExecutor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _parameters = new BuildRunnerParameters(TestData.GenerateString(), null, null, null);

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_parameters.Assembly)).Returns(_assembly.Object);

            _configurationResolver = new Mock<IConfigurationResolver>();

            _target = new Mock<ITarget>();
            _targetResolver = new Mock<ITargetResolver>();
            _targetResolver.Setup(tr => tr.Resolve(TargetConstants.DefaultTarget, _assembly.Object)).Returns(_target.Object);

            _targetExecutor = new Mock<ITargetExecutor>();
            _logger = new Mock<ILogger>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configurationResolver.Object, _targetResolver.Object, _targetExecutor.Object, _logger.Object);
        }

        protected override void Act()
        {
            Sut.Run(_parameters);
        }

        [Fact]
        public void Loads_the_assembly()
        {
            _assemblyLoader.Verify(al => al.Load(_parameters.Assembly));
        }

        [Fact]
        public void Resolves_the_configuration()
        {
            _configurationResolver.Verify(cr => cr.Resolve(_parameters.Configuration, _assembly.Object));
        }

        [Fact]
        public void Resolves_the_target()
        {
            _targetResolver.Verify(tr => tr.Resolve(TargetConstants.DefaultTarget, _assembly.Object));
        }

        [Fact]
        public void Executes_the_default_target()
        {
            _targetExecutor.Verify(te => te.Execute(_target.Object, null));
        }
    }
}