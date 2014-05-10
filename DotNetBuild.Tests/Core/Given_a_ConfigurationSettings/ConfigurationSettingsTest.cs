using System;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.Core.Given_a_ConfigurationSettings
{
    public class ConfigurationSettingsTest : ConfigurationSettings
    {
        public new void Add(String key, Object value)
        {
            base.Add(key, value);
        }
    }
}