using DotNetBuild.Core;

namespace DotNetBuild.Build.Configuration
{
    public class ConfigurationSettingsForTest : ConfigurationSettings
    {
        public ConfigurationSettingsForTest()
        {
            Add("MyProperty", @"ValueForTest");
        }
    }
}