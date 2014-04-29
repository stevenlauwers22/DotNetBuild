﻿using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRunner
{
    public class When_told_to_Run_with_an_empty_target
        : TestSpecification<BuildRunner>
    {
        private String _assemblyName;
        private String _targetName;
        private String _configurationName;
        private Mock<IAssemblyLoader> _assemblyLoader;
        private Mock<IAssemblyWrapper> _assembly;
        private Mock<IConfigurationResolver> _configurationResolver;
        private Mock<ITargetResolver> _targetResolver;
        private Mock<ITarget> _target;
        private Mock<ITargetExecutor> _targetExecutor;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _assemblyName = TestData.GenerateString();
            _targetName = null;
            _configurationName = null;

            _assembly = new Mock<IAssemblyWrapper>();
            _assemblyLoader = new Mock<IAssemblyLoader>();
            _assemblyLoader.Setup(al => al.Load(_assemblyName)).Returns(_assembly.Object);

            _target = new Mock<ITarget>();
            _targetResolver = new Mock<ITargetResolver>();
            _targetResolver.Setup(tr => tr.Resolve(TargetConstants.DefaultTarget, _assembly.Object)).Returns(_target.Object);

            _configurationResolver = new Mock<IConfigurationResolver>();

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
            _targetResolver.Verify(tr => tr.Resolve(TargetConstants.DefaultTarget, _assembly.Object));
        }

        [Fact]
        public void Resolves_the_configuration()
        {
            _configurationResolver.Verify(cr => cr.Resolve(_configurationName, _assembly.Object));
        }

        [Fact]
        public void Executes_the_default_target()
        {
            _targetExecutor.Verify(te => te.Execute(_target.Object, null));
        }
    }
}