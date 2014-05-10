using System;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationRegistry
{
    public class ConfigurationRegistryTest : ConfigurationRegistry
    {
        public new void Add(String key, IConfigurationSettings configurationSettings)
        {
            base.Add(key, configurationSettings);
        }
    }
}