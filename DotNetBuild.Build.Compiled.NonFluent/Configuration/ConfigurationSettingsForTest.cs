using DotNetBuild.Core;

namespace DotNetBuild.Build.Compiled.NonFluent.Configuration
{
    public class ConfigurationSettingsForTest : ConfigurationSettings
    {
        public ConfigurationSettingsForTest()
        {
            Add("MyProperty", @"ValueForTest");
        }
    }
}