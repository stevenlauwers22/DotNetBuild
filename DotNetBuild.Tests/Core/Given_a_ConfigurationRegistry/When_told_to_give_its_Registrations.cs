using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry
{
    public class When_told_to_give_its_Registrations
        : TestSpecification<ConfigurationRegistry>
    {
        private Mock<IConfigurationSettings> _configurationSettings;
        private IEnumerable<ConfigurationRegistration> _result;
        private Func<string, bool> _useIf;

        protected override void Arrange()
        {
            _configurationSettings = new Mock<IConfigurationSettings>();
            _useIf = configuration => true;
        }
        protected override ConfigurationRegistry CreateSubjectUnderTest()
        {
            return new ConfigurationRegistryTest(_configurationSettings.Object, _useIf);
        }

        protected override void Act()
        {
            _result = Sut.Registrations;
        }

        [Fact]
        public void Returns_its_registrations()
        {
            Assert.Equal(1, _result.Count());
            Assert.Equal(_configurationSettings.Object, _result.ElementAt(0).Configuration);
            Assert.Equal(_useIf, _result.ElementAt(0).UseIf);
        }

        private class ConfigurationRegistryTest : ConfigurationRegistry
        {
            public ConfigurationRegistryTest(IConfigurationSettings configurationSettings, Func<string, bool> useIf)
            {
                Add(configurationSettings, useIf);
            }
        }
    }
}