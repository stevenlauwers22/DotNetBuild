using System;
using DotNetBuild.Runner.Infrastructure;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration_with_no_assembly_wrapper
        : TestSpecification<ConfigurationResolver>
    {
        private string _configurationName;
        private IAssemblyWrapper _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private Mock<IConfigurationSelector> _configurationSelector;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();
            _assemblyWrapper = null;
            _typeActivator = new Mock<ITypeActivator>();
            _configurationSelector = new Mock<IConfigurationSelector>();
        }

        protected override ConfigurationResolver CreateSubjectUnderTest()
        {
            return new ConfigurationResolver(_configurationSelector.Object, _typeActivator.Object);
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => Sut.Resolve(_configurationName, _assemblyWrapper));
        }

        [Fact]
        public void Throws_an_exception()
        {
            Assert.NotNull(_exception);
            Assert.Equal("assembly", _exception.ParamName);
        }
    }
}