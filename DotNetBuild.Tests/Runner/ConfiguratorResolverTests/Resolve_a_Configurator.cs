using System;
using System.Reflection;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Reflection;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.ConfiguratorResolverTests
{
    public class Resolve_a_Configurator
        : TestSpecification<ConfiguratorResolver>
    {
        private Assembly _assembly;
        private Mock<IAssemblyWrapper> _assemblyWrapper;
        private Type _configuratorType;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<IConfigurator> _configurator;
        private IConfigurator _result;

        protected override void Arrange()
        {
            _assembly = GetType().Assembly;
            _assemblyWrapper = new Mock<IAssemblyWrapper>();
            _assemblyWrapper.Setup(aw => aw.Assembly).Returns(_assembly);

            _configuratorType = typeof(IConfigurator);
            _assemblyWrapper.Setup(a => a.Get<IConfigurator>()).Returns(_configuratorType);

            _configurator = new Mock<IConfigurator>();
            _typeActivator = new Mock<ITypeActivator>();
            _typeActivator.Setup(ta => ta.Activate<IConfigurator>(_configuratorType)).Returns(_configurator.Object);
        }

        protected override ConfiguratorResolver CreateSubjectUnderTest()
        {
            return new ConfiguratorResolver(_typeActivator.Object);
        }

        protected override void Act()
        {
            _result = Sut.Resolve(_assemblyWrapper.Object);
        }

        [Fact]
        public void Gets_the_Configurator_type()
        {
            _assemblyWrapper.Verify(a => a.Get<IConfigurator>());
        }

        [Fact]
        public void Instantiates_the_Configurator()
        {
            _typeActivator.Verify(ta => ta.Activate<IConfigurator>(_configuratorType));
        }

        [Fact]
        public void Returns_the_Configurator()
        {
            Assert.Equal(_configurator.Object, _result);
        }
    }
}