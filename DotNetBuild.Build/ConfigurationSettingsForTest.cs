using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class ConfigurationSettingsForTest : ConfigurationSettings
    {
        public ConfigurationSettingsForTest()
        {
            Add("MyProperty", @"ValueForTest");
        }
    }
}