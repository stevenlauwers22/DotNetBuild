﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Exceptions;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.BuildRunnerTests
{
    public class Run_with_no_target
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private String[] _parameters;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IAssemblyWrapper> _assembly;
        private Mock<IConfiguratorResolver> _configuratorResolver;
        private Mock<IConfigurator> _configurator;
        private Mock<IConfigurationRegistry> _configurationRegistry;
        private Mock<ITargetRegistry> _targetRegistry;
        private Mock<ITargetExecutor> _targetExecutor;
        private UnableToFindTargetException _exception;

        protected override void Arrange()
        {
            _assemblyName = TestData.GenerateString();
            _targetName = null;
            _configurationName = null;
            _parameters = null;

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_assemblyName)).Returns(_assembly.Object);
            
            _configurator = new Mock<IConfigurator>();
            _configuratorResolver = new Mock<IConfiguratorResolver>();
            _configuratorResolver.Setup(cr => cr.Resolve(_assembly.Object)).Returns(_configurator.Object);

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
            _exception = TestHelpers.CatchException<UnableToFindTargetException>(() => Sut.Run(_assemblyName, _targetName, _configurationName, _parameters));
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
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal(_targetName, _exception.Target);
        }
    }
}