using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRunner
{
    public class When_told_to_Run_with_valid_arguments
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IAssemblyWrapper> _assembly;
        private Mock<IConfigurationSettings> _configurationSettings;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITarget> _target;
        private Mock<ITargetExecutor> _targetExecutor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _assemblyName = TestData.GenerateString();
            _targetName = TestData.GenerateString();
            _configurationName = TestData.GenerateString();

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_assemblyName)).Returns(_assembly.Object);

            _target = new Mock<ITarget>();
            _targetResolver = new Mock<ITargetResolver>();
            _targetResolver.Setup(tr => tr.Resolve(_targetName, _assembly.Object)).Returns(_target.Object);

            _configurationSettings = new Mock<IConfigurationSettings>();
            _configurationResolver = new Mock<IConfigurationResolver>();
            _configurationResolver.Setup(csr => csr.Resolve(_configurationName, _assembly.Object)).Returns(_configurationSettings.Object);

            _targetExecutor = new Mock<ITargetExecutor>();
            _logger = new Mock<ILogger>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configurationResolver.Object, _targetResolver.Object, _targetExecutor.Object, _logger.Object);
        }

        protected override void Act()
        {
            Sut.Run(_assemblyName, _targetName, _configurationName);
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
        public void Resolves_the_configuration()
        {
            _configurationResolver.Verify(cr => cr.Resolve(_configurationName, _assembly.Object));
        }

        [Fact]
        public void Executes_the_specified_target()
        {
            _targetExecutor.Verify(te => te.Execute(_target.Object, _configurationSettings.Object));
        }
    }
}