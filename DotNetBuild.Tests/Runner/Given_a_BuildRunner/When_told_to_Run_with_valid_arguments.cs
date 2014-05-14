using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Reflection;
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
        private Mock<IConfiguratorResolver> _configuratorResolver;
        private Mock<IConfigurator> _configurator;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private Mock<IConfigurationSettings> _configurationSettings;
        private Mock<ITargetRegistry> _targetRegistry;
        private Mock<ITarget> _target;
        private Mock<ITargetExecutor> _targetExecutor;

        protected override void Arrange()
        {
            _assemblyName = TestData.GenerateString();
            _targetName = TestData.GenerateString();
            _configurationName = TestData.GenerateString();

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_assemblyName)).Returns(_assembly.Object);

            _configurator = new Mock<IConfigurator>();
            _configuratorResolver = new Mock<IConfiguratorResolver>();
            _configuratorResolver.Setup(cr => cr.Resolve(_assembly.Object)).Returns(_configurator.Object);

            _configurationSettings = new Mock<IConfigurationSettings>();
            _configurationRegistry = new Mock<IConfigurationRegistry>();
            _configurationRegistry.Setup(csr => csr.Get(_configurationName)).Returns(_configurationSettings.Object);

            _target = new Mock<ITarget>();
            _targetRegistry = new Mock<ITargetRegistry>();
            _targetRegistry.Setup(tr => tr.Get(_targetName)).Returns(_target.Object);

            _targetExecutor = new Mock<ITargetExecutor>();
        }

        protected override BuildRunner CreateSubjectUnderTest()
        {
            return new BuildRunner(_assemblyLoader.Object, _configuratorResolver.Object, _configurationRegistry.Object, _targetRegistry.Object, _targetExecutor.Object);
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
        public void Resolves_the_configurator()
        {
            _configuratorResolver.Verify(cr => cr.Resolve(_assembly.Object));
        }

        [Fact]
        public void Configures_the_configurator()
        {
            _configurator.Verify(c => c.Configure());
        }

        [Fact]
        public void Gets_the_target()
        {
            _targetRegistry.Verify(tr => tr.Get(_targetName));
        }

        [Fact]
        public void Gets_the_configuration()
        {
            _configurationRegistry.Verify(cr => cr.Get(_configurationName));
        }

        [Fact]
        public void Executes_the_specified_target()
        {
            _targetExecutor.Verify(te => te.Execute(_target.Object, _configurationSettings.Object));
        }
    }
}