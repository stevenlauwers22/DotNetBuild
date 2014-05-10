using System;
using DotNetBuild.Runner;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_ConfigurationResolver
{
    public class When_told_to_Resolve_a_Configuration_with_no_assembly_wrapper
        : TestSpecification<ConfigurationResolver>
    {
        private String _configurationName;
        private IAssemblyWrapper _assemblyWrapper;
        private Mock<ITypeActivator> _typeActivator;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _configurationName = TestData.GenerateString();
            _assemblyWrapper = null;
            _typeActivator = new Mock<ITypeActivator>();
        }

        protected override ConfigurationResolver CreateSubjectUnderTest()
        {
            return new ConfigurationResolver(_typeActivator.Object);
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