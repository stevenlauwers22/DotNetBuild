using System;
using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistration
{
    public class When_told_to_Get_its_UseIf
        : TestSpecification<ConfigurationRegistration>
    {
        private Func<string, bool> _useIf;
        private Func<string, bool> _result;

        protected override void Arrange()
        {
            _useIf = p => true;
        }

        protected override ConfigurationRegistration CreateSubjectUnderTest()
        {
            return new ConfigurationRegistration(new Mock<IConfigurationSettings>().Object, _useIf);
        }

        protected override void Act()
        {
            _result = Sut.UseIf;
        }

        [Fact]
        public void Returns_the_Configuration_passed_in_its_constructor()
        {
            Assert.Equal(_useIf, _result);
        }
    }
}